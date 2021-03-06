using Domain.Base.Entity;
using Domain.Common.Interfaces;
using Domain.OrderManagment.Events;
using Domain.OrderManagment.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.OrderManagment.AggregatesModel.OrderAggregate
{
    public class Order : AuditableEntity, IAggregateRoot
    {
        public string CustomerId { get; private set; }
        public string DistributorId { get; private set; }
        public string DistributorName { get; private set; }
        public int OrderNumber { get; private set; }
        public string CustomerName { get; private set; }
        public string CustomerShopName { get; private set; }
        public string CustomerShopAddress { get; private set; }
        public string CustomerCode { get; private set; }
        public string CustomerCity { get; private set; }
        public string CustomerArea { get; private set; }
        public string CustomerLocationOnMap { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public DateTime OrderPlacedDate { get; private set; }
        public DateTime OrderConfirmedDate { get; private set; }
        public DateTime OrderShippedDate { get; private set; }
        public DateTime OrderDeliveredDate { get; private set; }
        public DateTime OrderCanceledDate { get; private set; }
        public float TotalPrice { get; private set; }
        public ICollection<OrderItem> OrderItems { get; private set; }



        private Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public Order(string distributorId, string distributorName, string customerId, string customerName, string customerCode, string customerShopName, string customerShopAddress, string customerCity, string customerArea, string customerLocationOnMap)
        {
            CustomerId = customerId;
            DistributorId = distributorId;
            DistributorName = distributorName;
            CustomerName = customerName;
            CustomerCode = customerCode;
            CustomerShopName = customerShopName;
            CustomerShopAddress = customerShopAddress;
            CustomerCity = customerCity;
            CustomerArea = customerArea;
            CustomerLocationOnMap = customerLocationOnMap;
            OrderStatus = OrderStatus.Placed;
            OrderPlacedDate = DateTime.UtcNow;

            OrderItems = new List<OrderItem>();

            // Add the OrderStarterDomainEvent to the domain events collection 
            // to be raised/dispatched when commiting changes into the Database [ After DbContext.SaveChanges() ]
            // Register Placing order event
            AddOrderPlacedDomainEvent(customerId, customerShopAddress);
        }

        // DDD Patterns comment
        // This Order AggregateRoot's method "AddOrderItem()" should be the only way to add Items to the Order,
        // so any behavior (discounts, etc.) and validations are controlled by the AggregateRoot 
        // in order to maintain consistency between the whole Aggregate. 
        public void AddOrderItem(string productId, string productName, string brandName, float unitPrice, float unitSellingPrice, string photoUrl, string unitId, string unitName, int customerCount = 1)
        {
            var existingOrderForProduct = OrderItems.SingleOrDefault(o => o.ProductId == productId && o.UnitId == unitId);
            if (existingOrderForProduct != null) throw new ProductExitInOrderException(productId);

            //add validated new order item
            var orderItem = new OrderItem(Id.ToString(), productId, productName,brandName, unitPrice, unitSellingPrice, photoUrl, unitId, unitName, customerCount);

            OrderItems.Add(orderItem);

            ReCalcTotalOrderPrice();
        }

        private void ReCalcTotalOrderPrice()
        {
            TotalPrice = 0;
            foreach (var item in OrderItems)
            {
                TotalPrice += item.UnitSellingPrice * item.UnitCount;
            }
        }

        public void UpdateOrderItem(string orderItemId, string unitId, string unitName, float unitPrice, float unitSellingPrice, int cusotmerCount)
        {
            var orderItemToUpdate = OrderItems.FirstOrDefault(x => x.Id == new Guid(orderItemId));
            if (orderItemToUpdate == null) throw new OrderItemNotFoundException(orderItemId);


            orderItemToUpdate.Update(unitId, unitName, unitPrice, unitSellingPrice, cusotmerCount);


            AddDomainEvent(new OrderUpdated(this));

            ReCalcTotalOrderPrice();
        }

        public void ConfirmOrder()
        {
            OrderConfirmedDate = DateTime.UtcNow;
            OrderStatus = OrderStatus.Confirmed;

            // Register Confirmed order event
            AddOrderConfirmedDomainEvent();
        }

        // Shipping the order
        public void ShippOrder()
        {
            OrderShippedDate = DateTime.UtcNow;
            OrderStatus = OrderStatus.Shipped;

            // Register Shipped order event
            AddOrderShippedDomainEvent();
        }

        // Deliver the order
        public void DeliverOrder()
        {
            OrderDeliveredDate = DateTime.UtcNow;
            OrderStatus = OrderStatus.Delivered;

            // Register Delivered order event
            AddOrderDeliveredDomainEvent();
        }


        // Cancel the order
        public void CancelOrder()
        {
            OrderCanceledDate = DateTime.UtcNow;
            OrderStatus = OrderStatus.Cancelled;

            // Register Canceled order event
            AddOrderCanceledDomainEvent();
        }

        private void AddOrderPlacedDomainEvent(string customerId, string address)
        {
            var orderPlacededDomainEvent = new OrderPlaced(this, customerId, address);

            AddDomainEvent(orderPlacededDomainEvent);
        }

        private void AddOrderConfirmedDomainEvent()
        {
            var orderConfirmedDomainEvent = new OrderConfirmed(this);

            AddDomainEvent(orderConfirmedDomainEvent);
        }

        private void AddOrderShippedDomainEvent()
        {
            var orderShippedDomainEvent = new OrderShipped(this);

            AddDomainEvent(orderShippedDomainEvent);
        }

        private void AddOrderDeliveredDomainEvent()
        {
            var orderDeliveredDomainEvent = new OrderDelivered(this);

            AddDomainEvent(orderDeliveredDomainEvent);
        }

        private void AddOrderCanceledDomainEvent()
        {
            var orderCanceledDomainEvent = new OrderCancelled(this);

            AddDomainEvent(orderCanceledDomainEvent);
        }

    }

}

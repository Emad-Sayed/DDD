using Domain.Base.Entity;
using Domain.Common.Interfaces;
using Domain.OrderManagment.Events;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Domain.OrderManagment.AggregatesModel.OrderAggregate
{
    public class Order : EntityBase, IAggregateRoot
    {
        public string CustomerId { get; private set; }
        public string CustomerName { get; private set; }
        public string Address { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public DateTime OrderPlacedDate { get; set; }
        public DateTime OrderConfirmedDate { get; set; }
        public DateTime OrderShippedDate { get; set; }
        public DateTime OrderDeliveredDate { get; set; }
        public DateTime OrderCanceledDate { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }



        private Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public Order(string customerId, string customerName, string address)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            Address = address;
            OrderStatus = OrderStatus.Placed;
            OrderPlacedDate = DateTime.UtcNow;

            OrderItems = new List<OrderItem>();

            // Add the OrderStarterDomainEvent to the domain events collection 
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            // Register Placing order event
            AddOrderPlacedDomainEvent(customerId, address);
        }

        // DDD Patterns comment
        // This Order AggregateRoot's method "AddOrderitem()" should be the only way to add Items to the Order,
        // so any behavior (discounts, etc.) and validations are controlled by the AggregateRoot 
        // in order to maintain consistency between the whole Aggregate. 
        public void AddOrderItem(string productId, string productName, float unitPrice, string photoUrl, string unitId, string unitName, int unitCount = 1)
        {
            var existingOrderForProduct = OrderItems.Where(o => o.ProductId == productId)
                .SingleOrDefault();

            if (existingOrderForProduct != null)
            {
                //if previous line exist modify it with higher discount  and units..

                existingOrderForProduct.AddUnits(unitCount);
            }
            else
            {
                //add validated new order item

                var orderItem = new OrderItem(Id.ToString(), productId, productName, unitPrice, photoUrl, unitId, unitName, unitCount);
                OrderItems.Add(orderItem);
            }
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

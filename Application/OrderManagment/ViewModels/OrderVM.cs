using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OrderManagment.ViewModels
{
    public class OrderVM
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerShopName { get; set; }
        public string CustomerShopAddress { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerArea { get; set; }
        public string CustomerLocationOnMap { get; set; }
        public int OrderNumber { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime OrderPlacedDate { get; set; }
        public DateTime OrderConfirmedDate { get; set; }
        public DateTime OrderShippedDate { get; set; }
        public DateTime OrderDeliveredDate { get; set; }
        public DateTime OrderCanceledDate { get; set; }
        public float TotalPrice { get; set; }
        public ICollection<OrderItemVM> OrderItems { get; set; }
    }
}

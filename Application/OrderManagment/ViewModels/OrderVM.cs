using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OrderManagment.ViewModels
{
    public class OrderVM
    {
        public string Id { get; set; }
        public string CustomerId { get; private set; }
        public string CustomerName { get; private set; }
        public string Address { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public DateTime OrderPlacedDate { get; set; }
        public DateTime OrderConfirmedDate { get; set; }
        public DateTime OrderShippedDate { get; set; }
        public DateTime OrderDeliveredDate { get; set; }
        public DateTime OrderCanceledDate { get; set; }
        public float TotalPrice { get; set; }
        public ICollection<OrderItemVM> OrderItems { get; set; }
    }
}

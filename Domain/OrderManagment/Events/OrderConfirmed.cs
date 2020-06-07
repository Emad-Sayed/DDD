using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using MediatR;
using System.Collections.Generic;

namespace Domain.OrderManagment.Events
{
    /// <summary>
    /// Event used when the order is Confirmed
    /// </summary>
    public class OrderConfirmed : INotification
    {
        public Order Order { get; set; }
        public OrderConfirmed(Order order)
        {
            Order = order;
        }
    }
}

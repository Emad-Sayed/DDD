using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.OrderManagment.Events
{
    /// <summary>
    /// Event used when the order stock items are confirmed
    /// </summary>
    public class OrderDelivered
        : INotification
    {
        public Order Order { get; }

        public OrderDelivered(Order order)
            => Order = order;
    }
}

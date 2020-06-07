using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.OrderManagment.Events
{
    public class OrderShipped : INotification
    {
        public Order Order { get; }

        public OrderShipped(Order order)
        {
            Order = order;
        }
    }
}

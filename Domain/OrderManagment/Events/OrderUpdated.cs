using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.OrderManagment.Events
{
    public class OrderUpdated : INotification
    {
        public Order Order { get; }

        public OrderUpdated(Order order)
        {
            Order = order;
        }
    }
}

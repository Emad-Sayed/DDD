using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.OrderManagment.Events
{
    public class OrderCancelled : INotification
    {
        public Order Order { get; }

        public OrderCancelled(Order order)
        {
            Order = order;
        }
    }
}

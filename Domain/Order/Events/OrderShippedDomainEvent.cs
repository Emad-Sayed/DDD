using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Order.Events
{
    public class OrderShippedDomainEvent : INotification
    {
        public AggregatesModel.OrderAggregate.Order Order { get; }

        public OrderShippedDomainEvent(AggregatesModel.OrderAggregate.Order order)
        {
            Order = order;
        }
    }
}

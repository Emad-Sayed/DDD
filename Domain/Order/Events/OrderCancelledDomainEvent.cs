using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Order.Events
{
    public class OrderCancelledDomainEvent : INotification
    {
        public AggregatesModel.OrderAggregate.Order Order { get; }

        public OrderCancelledDomainEvent(AggregatesModel.OrderAggregate.Order order)
        {
            Order = order;
        }
    }
}

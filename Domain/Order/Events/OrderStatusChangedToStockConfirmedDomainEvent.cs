using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Order.Events
{
    /// <summary>
    /// Event used when the order stock items are confirmed
    /// </summary>
    public class OrderStatusChangedToStockConfirmedDomainEvent
        : INotification
    {
        public string OrderId { get; }

        public OrderStatusChangedToStockConfirmedDomainEvent(string orderId)
            => OrderId = orderId;
    }
}

using Domain.OrderManagment.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderManagment.DomainEventHandlers.OrderCancelledDomainEventHandlers
{
    public class OrderCancelledDomainEventHandler : INotificationHandler<OrderCancelled>
    {

        public OrderCancelledDomainEventHandler()
        {
        }

        public Task Handle(OrderCancelled notification, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}

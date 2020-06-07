using Domain.OrderManagment.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderManagment.DomainEventHandlers.OrderConfirmedDomainEventHandlers
{
    public class OrderConfirmedDomainEventHandler : INotificationHandler<OrderConfirmed>
    {

        public OrderConfirmedDomainEventHandler()
        {
        }

        public Task Handle(OrderConfirmed notification, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}

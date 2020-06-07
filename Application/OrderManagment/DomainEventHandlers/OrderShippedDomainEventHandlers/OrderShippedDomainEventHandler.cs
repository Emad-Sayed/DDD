using Domain.OrderManagment.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderManagment.DomainEventHandlers.OrderShippedDomainEventHandlers
{
    public class OrderShippedDomainEventHandler : INotificationHandler<OrderShipped>
    {

        public OrderShippedDomainEventHandler()
        {
        }

        public Task Handle(OrderShipped notification, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}

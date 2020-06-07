using Domain.OrderManagment.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderManagment.DomainEventHandlers.OrderPlacedDomainEventHandlers
{
    public class OrderPlacedDomainEventHandler : INotificationHandler<OrderPlaced>
    {

        public OrderPlacedDomainEventHandler()
        {
        }

        public Task Handle(OrderPlaced notification, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}

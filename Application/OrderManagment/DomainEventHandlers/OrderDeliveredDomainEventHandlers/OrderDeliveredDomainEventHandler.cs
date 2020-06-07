using Domain.OrderManagment.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderManagment.DomainEventHandlers.OrderDeliveredDomainEventHandlers
{
    public class OrderDeliveredDomainEventHandler : INotificationHandler<OrderDelivered>
    {

        public OrderDeliveredDomainEventHandler()
        {
        }

        public Task Handle(OrderDelivered notification, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}

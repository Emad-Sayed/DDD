using Domain.OrderManagment.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderManagment.DomainEventHandlers.OrderUpdatedDomainEventHandlers
{
    public class OrderUpdatedDomainEventHandler : INotificationHandler<OrderUpdated>
    {

        public OrderUpdatedDomainEventHandler()
        {
        }

        public Task Handle(OrderUpdated notification, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}

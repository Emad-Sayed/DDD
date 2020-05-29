using Domain.ShoppingVanBoundedContext.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ShoppingVanBoundedContext.DomainEventHandlers.ShoppingVanCreatedDomainEventHandlers
{
    public class ShoppingVanCreatedDomainEventHandler : INotificationHandler<ShoppingVanCreated>
    {

        public ShoppingVanCreatedDomainEventHandler()
        {
        }

        public Task Handle(ShoppingVanCreated notification, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}

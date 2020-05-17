using Domain.ProductCatalog.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.DomainEventHandlers.BrandCreatedDomainEventHandlers
{
    public class BrandCreatedDomainEventHandler : INotificationHandler<BrandCreated>
    {
       
        public BrandCreatedDomainEventHandler()
        {
        }

        public Task Handle(BrandCreated notification, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}

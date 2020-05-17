using Domain.ProductCatalog.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.DomainEventHandlers.ProductCategoryCreatedDomainEventHandlers
{
    public class ProductCategoryCreatedDomainEventHandler : INotificationHandler<ProductCategoryCreated>
    {

        public ProductCategoryCreatedDomainEventHandler()
        {
        }

        public Task Handle(ProductCategoryCreated notification, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}

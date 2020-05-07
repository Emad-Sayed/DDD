using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.Products.Commands.CreateProduct
{
    public class ProductCreated : INotification
    {

        public class ProductCreatedHandler : INotificationHandler<ProductCreated>
        {
            public ProductCreatedHandler()
            {
            }

            public Task Handle(ProductCreated notification, CancellationToken cancellationToken)
            {
                return Task.CompletedTask;
            }
        }
    }
}

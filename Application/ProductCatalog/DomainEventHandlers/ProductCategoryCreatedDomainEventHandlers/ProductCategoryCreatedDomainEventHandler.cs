using Application.Common.Interfaces;
using Domain.ProductCatalog.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.DomainEventHandlers.ProductCategoryCreatedDomainEventHandlers
{
    public class ProductCategoryCreatedDomainEventHandler : INotificationHandler<ProductCategoryCreated>
    {

        private readonly ILogger<ProductCategoryCreatedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public ProductCategoryCreatedDomainEventHandler(ILogger<ProductCategoryCreatedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Handle(ProductCategoryCreated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(ProductCategoryCreated), _currentUserService.UserId, _currentUserService.Name, notification);

            return Task.CompletedTask;
        }
    }
}

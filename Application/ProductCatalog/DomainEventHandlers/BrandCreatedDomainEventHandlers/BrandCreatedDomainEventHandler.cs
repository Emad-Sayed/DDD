using Application.Common.Interfaces;
using Domain.ProductCatalog.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.DomainEventHandlers.BrandCreatedDomainEventHandlers
{
    public class BrandCreatedDomainEventHandler : INotificationHandler<BrandCreated>
    {
        private readonly ILogger<BrandCreatedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public BrandCreatedDomainEventHandler(ILogger<BrandCreatedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Handle(BrandCreated notification, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(BrandCreated), _currentUserService.UserId, _currentUserService.Name, notification);
            return Task.CompletedTask;
        }
    }
}

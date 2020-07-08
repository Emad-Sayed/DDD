using Application.Common.Interfaces;
using Domain.ShoppingVanBoundedContext.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ShoppingVanBoundedContext.DomainEventHandlers.ShoppingVanCreatedDomainEventHandlers
{
    public class ShoppingVanCreatedDomainEventHandler : INotificationHandler<ShoppingVanCreated>
    {
        private readonly ILogger<ShoppingVanCreatedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public ShoppingVanCreatedDomainEventHandler(ILogger<ShoppingVanCreatedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Handle(ShoppingVanCreated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(ShoppingVanCreated), _currentUserService.UserId, _currentUserService.Name, notification);

            return Task.CompletedTask;
        }
    }
}

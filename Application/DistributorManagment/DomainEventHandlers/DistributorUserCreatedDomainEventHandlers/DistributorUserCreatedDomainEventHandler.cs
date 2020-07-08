using Application.Common.Interfaces;
using Domain.DistributorManagment.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.DomainEventHandlers.DistributorUserCreatedDomainEventHandlers
{
    public class DistributorUserCreatedDomainEventHandler : INotificationHandler<DistributorUserCreated>
    {
        private readonly ILogger<DistributorUserCreatedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public DistributorUserCreatedDomainEventHandler(ILogger<DistributorUserCreatedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Handle(DistributorUserCreated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(DistributorUserCreated), _currentUserService.UserId, _currentUserService.Name, notification);

            return Task.CompletedTask;
        }
    }
}

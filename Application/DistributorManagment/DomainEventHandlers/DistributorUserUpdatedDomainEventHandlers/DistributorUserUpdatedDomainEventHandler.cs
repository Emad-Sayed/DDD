using Application.Common.Interfaces;
using Domain.DistributorManagment.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.DomainEventHandlers.DistributorUserUpdatedDomainEventHandlers
{
    public class DistributorUserUpdatedDomainEventHandler : INotificationHandler<DistributorUserUpdated>
    {
        private readonly ILogger<DistributorUserUpdatedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public DistributorUserUpdatedDomainEventHandler(ILogger<DistributorUserUpdatedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Handle(DistributorUserUpdated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(DistributorUserUpdated), _currentUserService.UserId, _currentUserService.Name, notification);

            return Task.CompletedTask;
        }
    }
}

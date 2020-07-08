using Application.Common.Interfaces;
using Domain.DistributorManagment.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.DomainEventHandlers.DistributorCreatedDomainEventHandlers
{
    public class DistributorCreatedDomainEventHandler : INotificationHandler<DistributorCreated>
    {
        private readonly ILogger<DistributorCreatedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public DistributorCreatedDomainEventHandler(ILogger<DistributorCreatedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Handle(DistributorCreated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(DistributorCreated), _currentUserService.UserId, _currentUserService.Name, notification);

            return Task.CompletedTask;
        }
    }
}

using Application.Common.Interfaces;
using Domain.DistributorManagment.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.DomainEventHandlers.DistributorDeletedDomainEventHandlers
{
    public class DistributorDeletedDomainEventHandler : INotificationHandler<DistributorDeleted>
    {
        private readonly ILogger<DistributorDeletedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public DistributorDeletedDomainEventHandler(ILogger<DistributorDeletedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Handle(DistributorDeleted notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(DistributorDeleted), _currentUserService.UserId, _currentUserService.Name, notification);

            return Task.CompletedTask;
        }
    }
}

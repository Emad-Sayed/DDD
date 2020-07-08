using Application.Common.Interfaces;
using Domain.DistributorManagment.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.DomainEventHandlers.DistributorUserEmailConfirmedDomainEventHandlers
{
    public class DistributorUserEmailConfirmedDomainEventHandler : INotificationHandler<DistributorUserEmailConfirmed>
    {
        private readonly ILogger<DistributorUserEmailConfirmedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public DistributorUserEmailConfirmedDomainEventHandler(ILogger<DistributorUserEmailConfirmedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Handle(DistributorUserEmailConfirmed notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(DistributorUserEmailConfirmed), _currentUserService.UserId, _currentUserService.Name, notification);

            return Task.CompletedTask;
        }
    }
}

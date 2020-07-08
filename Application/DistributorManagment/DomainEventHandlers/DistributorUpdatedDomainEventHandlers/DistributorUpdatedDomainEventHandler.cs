using Application.Common.Interfaces;
using Domain.DistributorManagment.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.DomainEventHandlers.DistributorUpdatedDomainEventHandlers
{
    public class DistributorUpdatedDomainEventHandler : INotificationHandler<DistributorUpdated>
    {
        private readonly ILogger<DistributorUpdatedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public DistributorUpdatedDomainEventHandler(ILogger<DistributorUpdatedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Handle(DistributorUpdated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(DistributorUpdated), _currentUserService.UserId, _currentUserService.Name, notification);

            return Task.CompletedTask;
        }
    }
}

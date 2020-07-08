using Application.Common.Interfaces;
using Domain.CustomerManagment.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CustomerManagment.DomainEventHandlers.CustomerUpdatedDomainEventHandlers
{
    public class CustomerUpdatedDomainEventHandler : INotificationHandler<CustomerUpdated>
    {
        private readonly ILogger<CustomerUpdatedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public CustomerUpdatedDomainEventHandler(ILogger<CustomerUpdatedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Handle(CustomerUpdated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(CustomerUpdated), _currentUserService.UserId, _currentUserService.Name, notification);

            return Task.CompletedTask;
        }
    }
}

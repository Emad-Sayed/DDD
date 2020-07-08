using Application.Common.Interfaces;
using Domain.CustomerManagment.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CustomerManagment.DomainEventHandlers.CustomerCreatedDomainEventHandlers
{
    public class CustomerCreatedDomainEventHandler : INotificationHandler<CustomerCreated>
    {
        private readonly ILogger<CustomerCreatedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public CustomerCreatedDomainEventHandler(ILogger<CustomerCreatedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Handle(CustomerCreated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(CustomerCreated), _currentUserService.UserId, _currentUserService.Name, notification);

            return Task.CompletedTask;
        }
    }
}

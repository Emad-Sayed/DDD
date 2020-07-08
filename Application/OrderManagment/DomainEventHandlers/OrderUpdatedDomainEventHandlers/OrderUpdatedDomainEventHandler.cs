using Application.Common.Interfaces;
using Domain.OrderManagment.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderManagment.DomainEventHandlers.OrderUpdatedDomainEventHandlers
{
    public class OrderUpdatedDomainEventHandler : INotificationHandler<OrderUpdated>
    {
        private readonly ILogger<OrderUpdatedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public OrderUpdatedDomainEventHandler(ILogger<OrderUpdatedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Handle(OrderUpdated notification, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(OrderUpdated), _currentUserService.UserId, _currentUserService.Name, notification);
            return Task.CompletedTask;
        }
    }
}

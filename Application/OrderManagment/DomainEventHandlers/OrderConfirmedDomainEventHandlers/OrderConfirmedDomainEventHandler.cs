using Application.Common.Interfaces;
using Domain.OrderManagment.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderManagment.DomainEventHandlers.OrderConfirmedDomainEventHandlers
{
    public class OrderConfirmedDomainEventHandler : INotificationHandler<OrderConfirmed>
    {
        private readonly ILogger<OrderConfirmedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        public OrderConfirmedDomainEventHandler(ILogger<OrderConfirmedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Handle(OrderConfirmed notification, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(OrderConfirmed), _currentUserService.UserId, _currentUserService.Name, notification);
            return Task.CompletedTask;
        }
    }
}

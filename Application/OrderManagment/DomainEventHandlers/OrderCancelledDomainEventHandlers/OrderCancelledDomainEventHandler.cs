using Application.Common.Interfaces;
using Domain.OrderManagment.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderManagment.DomainEventHandlers.OrderCancelledDomainEventHandlers
{
    public class OrderCancelledDomainEventHandler : INotificationHandler<OrderCancelled>
    {
        private readonly ILogger<OrderCancelledDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        public OrderCancelledDomainEventHandler(ILogger<OrderCancelledDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Handle(OrderCancelled notification, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(OrderCancelled), _currentUserService.UserId, _currentUserService.Name, notification);
            return Task.CompletedTask;
        }
    }
}

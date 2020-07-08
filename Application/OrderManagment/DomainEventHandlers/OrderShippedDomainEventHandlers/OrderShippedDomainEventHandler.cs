using Application.Common.Interfaces;
using Domain.OrderManagment.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderManagment.DomainEventHandlers.OrderShippedDomainEventHandlers
{
    public class OrderShippedDomainEventHandler : INotificationHandler<OrderShipped>
    {
        private readonly ILogger<OrderShippedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public OrderShippedDomainEventHandler(ILogger<OrderShippedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Handle(OrderShipped notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(OrderShipped), _currentUserService.UserId, _currentUserService.Name, notification);

            return Task.CompletedTask;
        }
    }
}

using Application.Common.Interfaces;
using Domain.OrderManagment.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderManagment.DomainEventHandlers.OrderDeliveredDomainEventHandlers
{
    public class OrderDeliveredDomainEventHandler : INotificationHandler<OrderDelivered>
    {
        private readonly ILogger<OrderDeliveredDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public OrderDeliveredDomainEventHandler(ILogger<OrderDeliveredDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Handle(OrderDelivered notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(OrderDelivered), _currentUserService.UserId, _currentUserService.Name, notification);

            return Task.CompletedTask;
        }
    }
}

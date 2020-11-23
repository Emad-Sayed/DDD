using Application.Common.Interfaces;
using Application.NotificationManagment.Commands.CreateNotification;
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
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        public OrderConfirmedDomainEventHandler(ILogger<OrderConfirmedDomainEventHandler> logger, ICurrentUserService currentUserService, IMediator mediator)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public async Task Handle(OrderConfirmed notification, CancellationToken cancellationToken)
        {
            var createNotificationCommand = new CreateNotificationCommand { Title = " تاكيد الطلب", Content = "تم تاكيد الطلب الخاص بك", EntityId = notification.Order.Id.ToString(), NotificationType = 0, CustomerId = notification.Order.CustomerId };

            await _mediator.Send(createNotificationCommand);

            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(OrderConfirmed), _currentUserService.UserId, _currentUserService.Name, notification);

        }
    }
}

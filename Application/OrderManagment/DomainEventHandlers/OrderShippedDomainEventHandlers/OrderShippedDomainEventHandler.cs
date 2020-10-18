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

namespace Application.OrderManagment.DomainEventHandlers.OrderShippedDomainEventHandlers
{
    public class OrderShippedDomainEventHandler : INotificationHandler<OrderShipped>
    {
        private readonly ILogger<OrderShippedDomainEventHandler> _logger;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public OrderShippedDomainEventHandler(ILogger<OrderShippedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task Handle(OrderShipped notification, CancellationToken cancellationToken)
        {
            var createNotificationCommand = new CreateNotificationCommand { Title = "تم شجن الطلب", Content = "تم شحن الطلب الخاص بك", EntityId = notification.Order.Id.ToString(), NotificationType = 0 };

            await _mediator.Send(createNotificationCommand);

            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(OrderShipped), _currentUserService.UserId, _currentUserService.Name, notification);
        }
    }
}

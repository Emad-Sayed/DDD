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

namespace Application.OrderManagment.DomainEventHandlers.OrderCancelledDomainEventHandlers
{
    public class OrderCancelledDomainEventHandler : INotificationHandler<OrderCancelled>
    {
        private readonly ILogger<OrderCancelledDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;
        public OrderCancelledDomainEventHandler(ILogger<OrderCancelledDomainEventHandler> logger, ICurrentUserService currentUserService, IMediator mediator)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public async Task Handle(OrderCancelled notification, CancellationToken cancellationToken)
        {
            var createNotificationCommand = new CreateNotificationCommand { Title = " إلغاء الطلب", Content = "تم الغاء الطلب الخاص بك", EntityId = notification.Order.Id.ToString(), NotificationType = 0, CustomerId = notification.Order.CustomerId };

            await _mediator.Send(createNotificationCommand);

            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(OrderCancelled), _currentUserService.UserId, _currentUserService.Name, notification);
        }
    }
}

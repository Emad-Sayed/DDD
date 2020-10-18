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

namespace Application.OrderManagment.DomainEventHandlers.OrderDeliveredDomainEventHandlers
{
    public class OrderDeliveredDomainEventHandler : INotificationHandler<OrderDelivered>
    {
        private readonly ILogger<OrderDeliveredDomainEventHandler> _logger;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public OrderDeliveredDomainEventHandler(ILogger<OrderDeliveredDomainEventHandler> logger, ICurrentUserService currentUserService, IMediator mediator)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public async Task Handle(OrderDelivered notification, CancellationToken cancellationToken)
        {
            var createNotificationCommand = new CreateNotificationCommand { Title = " توصيل الطلب", Content = "تم توصيل الطلب الخاص بك", EntityId = notification.Order.Id.ToString(), NotificationType = 0 };

            await _mediator.Send(createNotificationCommand);

            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(OrderDelivered), _currentUserService.UserId, _currentUserService.Name, notification);
        }
    }
}

using Application.Common.Interfaces;
using Application.CustomerManagment.Queries.GetMyDevicesIds;
using AutoMapper;
using Domain.Common.Interfaces;
using Domain.NotificationManagment.AggregatesModel;
using Domain.NotificationManagment.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.NotificationManagment.DomainEventHandlers.NotificationCreatedDomainEventHandlers
{
    public class NotificationCreatedDomainEventHandler : INotificationHandler<NotificationCreated>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationService _notificationService;
        private readonly IMediator _mediator;
        private readonly ILogger<NotificationCreatedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public NotificationCreatedDomainEventHandler(INotificationRepository notificationRepository, ILogger<NotificationCreatedDomainEventHandler> logger, ICurrentUserService currentUserService, INotificationService notificationService, IMediator mediator)
        {
            _notificationRepository = notificationRepository;
            _logger = logger;
            _currentUserService = currentUserService;
            _notificationService = notificationService;
            _mediator = mediator;
        }

        public async Task Handle(NotificationCreated notification, CancellationToken cancellationToken)
        {

            var deviceIDQuery = new GetCustomerDeviceId { CustomerId = notification.Notification.UserId };

            var deviceId = await _mediator.Send(deviceIDQuery);

            await _notificationService.Send(notification.Notification, new List<string> { deviceId });

            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(NotificationCreated), _currentUserService.UserId, _currentUserService.Name, notification);

        }
    }
}

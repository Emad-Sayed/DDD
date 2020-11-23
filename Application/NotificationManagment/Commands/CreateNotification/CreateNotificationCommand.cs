using Application.Common.Interfaces;
using Domain.NotificationManagment.AggregatesModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.NotificationManagment.Commands.CreateNotification
{
    public class CreateNotificationCommand : IRequest<string>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string EntityId { get; set; }
        public NotificationType NotificationType { get; set; }
        public string CustomerId { get; set; }

        public class Handler : IRequestHandler<CreateNotificationCommand, string>
        {
            private readonly INotificationRepository _notificationRepository;
            private readonly ICurrentUserService _currentUserService;

            public Handler(INotificationRepository notificationRepository, ICurrentUserService currentUserService)
            {
                _notificationRepository = notificationRepository;
                _currentUserService = currentUserService;
            }

            public async Task<string> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
            {

                var newNotificationToAdd = new Notification(request.Title, request.Content, request.EntityId, request.CustomerId, request.NotificationType);

                _notificationRepository.Add(newNotificationToAdd);

                await _notificationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return newNotificationToAdd.Id.ToString();
            }
        }
    }
}

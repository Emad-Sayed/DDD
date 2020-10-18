using Domain.NotificationManagment.AggregatesModel;
using Domain.NotificationManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.NotificationManagment.Commands.ReadNotification
{
    public class ReadNotificationCommand : IRequest<string>
    {
        public string NotificationId { get; set; }

        public class Handler : IRequestHandler<ReadNotificationCommand, string>
        {
            private readonly INotificationRepository _notificationRepository;

            public Handler(INotificationRepository notificationRepository)
            {
                _notificationRepository = notificationRepository;
            }

            public async Task<string> Handle(ReadNotificationCommand request, CancellationToken cancellationToken)
            {

                var notification = await _notificationRepository.FindByIdAsync(request.NotificationId);
                if (notification == null) throw new NotificationNotFoundException(request.NotificationId);

                notification.ReadNotification();

                _notificationRepository.Update(notification);

                await _notificationRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return notification.Id.ToString();
            }
        }
    }
}

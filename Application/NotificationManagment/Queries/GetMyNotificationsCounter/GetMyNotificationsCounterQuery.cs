using Application.Common.Interfaces;
using Application.NotificationManagment.ViewModels;
using Domain.NotificationManagment.AggregatesModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.NotificationManagment.Queries.GetMyNotificationsCount
{
    public class GetMyNotificationsCounterQuery : IRequest<NotificationCounterVM>
    {
        public class Handler : IRequestHandler<GetMyNotificationsCounterQuery, NotificationCounterVM>
        {
            private readonly ICurrentUserService _currentUserService;
            private readonly INotificationRepository _notificationsRepository;

            public Handler(INotificationRepository notificationsRepository, ICurrentUserService currentUserService)
            {
                _notificationsRepository = notificationsRepository;
                _currentUserService = currentUserService;
            }

            public async Task<NotificationCounterVM> Handle(GetMyNotificationsCounterQuery request, CancellationToken cancellationToken)
            {
                var (total, readed, unReded) = _notificationsRepository.GetMyNotificationCounterAsync(_currentUserService.UserId);
                var notificationsCounterVM = new NotificationCounterVM
                {
                    TotalNotifications = total,
                    TotalReadedNotifications = readed,
                    TotalUnReadedNotifications = unReded
                };

                return notificationsCounterVM;
            }
        }
    }
}

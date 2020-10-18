using Application.Common.Interfaces;
using Application.Common.Models;
using Application.NotificationManagment.ViewModels;
using AutoMapper;
using Domain.NotificationManagment.AggregatesModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.NotificationManagment.Queries.GetMyNotifications
{
    public class GetMyNotificationsQuery : IRequest<ListEntityVM<NotificationVM>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string KeyWord { get; set; }

        public class Handler : IRequestHandler<GetMyNotificationsQuery, ListEntityVM<NotificationVM>>
        {
            private readonly ICurrentUserService _currentUserService;
            private readonly INotificationRepository _notificationsRepository;
            private readonly IMapper _mapper;

            public Handler(INotificationRepository notificationsRepository, IMapper mapper, ICurrentUserService currentUserService)
            {
                _notificationsRepository = notificationsRepository;
                _mapper = mapper;
                _currentUserService = currentUserService;
            }

            public async Task<ListEntityVM<NotificationVM>> Handle(GetMyNotificationsQuery request, CancellationToken cancellationToken)
            {
                var notificationsFromRepo = await _notificationsRepository.GetAllAsync(_currentUserService.UserId, request.PageNumber, request.PageSize, request.KeyWord);

                var notificationsToReturn = _mapper.Map<List<NotificationVM>>(notificationsFromRepo.Item2);

                return new ListEntityVM<NotificationVM> { TotalCount = notificationsFromRepo.Item1, Data = notificationsToReturn };
            }
        }
    }
}

using Application.NotificationManagment.ViewModels;
using Domain.NotificationManagment.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.NotificationManagment
{
    public class NotificationMappingProfile : AutoMapper.Profile
    {
        public NotificationMappingProfile()
        {
            CreateMap<Notification, NotificationVM>().ReverseMap();
        }
    }
}

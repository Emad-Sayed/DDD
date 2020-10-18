using Domain.NotificationManagment.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Interfaces
{
    public interface INotificationService
    {
        Task Send(Notification notification, List<string> DevicesIds);
    }
}
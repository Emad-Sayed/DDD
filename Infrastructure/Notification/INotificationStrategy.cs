using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Notification
{
    public interface INotificationStrategy
    {
        void Notify(NotificationModel notificationModel);
    }
}

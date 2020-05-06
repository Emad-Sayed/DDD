using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Interfaces
{
    public interface IUserNotification
    {
        // TODO : get notification content
        void NotifyUser(NotificationModel model);
    }
}

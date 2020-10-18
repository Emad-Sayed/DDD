using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.NotificationManagment.Exceptions
{
    public class NotificationNotFoundException : BusinessException
    {
        public NotificationNotFoundException(string id)
           : base(HttpStatusCode.NotFound, $"Notification with id {id} was not found.", "notification_notfound")
        {
        }
    }
}

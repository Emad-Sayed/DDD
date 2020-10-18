using Domain.NotificationManagment.AggregatesModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.NotificationManagment.Events
{
    public class NotificationCreated : INotification
    {
        public Notification Notification { get; private set; }

        public NotificationCreated(Notification notification)
        {
            Notification = notification;
        }
    }
}

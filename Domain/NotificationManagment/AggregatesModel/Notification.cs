using Domain.Base.Entity;
using Domain.Common.Interfaces;
using Domain.NotificationManagment.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.NotificationManagment.AggregatesModel
{
    public class Notification : AuditableEntity, IAggregateRoot
    {
        public string Title { get; private set; }
        public string Content { get; private set; }
        public string EntityId { get; private set; }
        public bool IsReaded { get; private set; }
        public string UserId { get; private set; }

        public NotificationType NotificationType { get; private set; }

        public Notification(string title, string content, string entityId, string userId, NotificationType notificationType, Guid id = default)
        {
            Title = title;
            Content = content;
            EntityId = entityId;
            IsReaded = false;
            UserId = userId;
            NotificationType = notificationType;

            Id = id == default ? Guid.NewGuid() : id;

            // Add the NotificationCreated to the domain events collection 
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            AddDomainEvent(new NotificationCreated(this));
        }

        // read notification
        public void ReadNotification()
        {
            IsReaded = true;

            // rais notification updated event
            AddDomainEvent(new NotificationReaded(this));
        }

    }
}

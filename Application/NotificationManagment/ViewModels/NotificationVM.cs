using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.NotificationManagment.ViewModels
{
    public class NotificationVM
    {
        public string Id { get; set; }
        public string CreateDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string EntityId { get; set; }
        public bool IsReaded { get; set; }
        public string UserId { get; set; }

        public NotificationType NotificationType { get; set; }
    }
}

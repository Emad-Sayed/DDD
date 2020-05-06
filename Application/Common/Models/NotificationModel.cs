using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Models
{
    public class NotificationModel
    {
        public string Key { get; set; }
        public List<string> Body { get; set; }
        public string Title { get; set; }
        public string Action { get; set; }
        public List<string> QuickActions { get; set; }
        public string Receiver { get; set; }
        public NotificationType Type { get; set; }
    }
}

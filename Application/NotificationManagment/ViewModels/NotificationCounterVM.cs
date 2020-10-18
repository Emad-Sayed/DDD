using System;
using System.Collections.Generic;
using System.Text;

namespace Application.NotificationManagment.ViewModels
{
    public class NotificationCounterVM
    {
        public int TotalNotifications { get; set; }
        public int TotalUnReadedNotifications { get; set; }
        public int TotalReadedNotifications { get; set; }
    }
}

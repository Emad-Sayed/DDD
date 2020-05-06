using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Notification
{
    public class EmailConfigurations
    {
        public bool EnableSSL;
        public string SenderEmail { get; set; }
        public string SenderPassword { get; set; }
        public string SenderDisplayName { get; set; }
        public string ServerHostName { get; set; }
        public int ServerPortNumber { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.SMSMessage
{
    // Configurations for twilio rest api
    public class TwilioSMSConfigurations
    {
        /// <summary>
        /// Account unique identification for a twilio app
        /// </summary>
        public string AccountSID { get; set; }

        /// <summary>
        /// Auth token to send to twilio api
        /// </summary>
        public string AuthToken { get; set; }

        /// <summary>
        /// Application Phone number 
        /// </summary>
        public string FromPhoneNumber { get; set; }
    }
}

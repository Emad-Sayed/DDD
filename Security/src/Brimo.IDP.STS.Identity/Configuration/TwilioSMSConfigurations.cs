using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brimo.IDP.STS.Identity.Configuration
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

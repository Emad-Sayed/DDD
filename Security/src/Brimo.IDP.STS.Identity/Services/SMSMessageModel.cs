using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brimo.IDP.STS.Identity.Services
{
    public class SMSMessageModel
    {
        public string ToPhoneNumber { get; set; }
        public string Message { get; set; }
    }
}

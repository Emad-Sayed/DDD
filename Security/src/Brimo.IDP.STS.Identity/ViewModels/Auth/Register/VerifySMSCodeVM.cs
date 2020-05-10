using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brimo.IDP.STS.Identity.ViewModels.Auth.Register
{
    public class VerifySMSCodeVM
    {
        public string PhoneNumber { get; set; }
        public string SmsCode { get; set; }
    }
}

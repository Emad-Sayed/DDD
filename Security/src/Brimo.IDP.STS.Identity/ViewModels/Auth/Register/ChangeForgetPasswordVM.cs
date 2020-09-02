using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brimo.IDP.STS.Identity.ViewModels.Auth.Register
{
    public class ChangeForgetPasswordVM
    {
        public string PhoneNumber { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brimo.IDP.STS.Identity.ViewModels.Auth.Register
{
    public class ConfirmEmailVM
    {
        public string Token { get; set; }
        public string Email { get; set; }
    }
}

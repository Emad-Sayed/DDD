using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Brimo.IDP.STS.Identity.Common.Exceptions.Auth
{
    public class InvalidForgetPasswordCodeException : BusinessException
    {
        public InvalidForgetPasswordCodeException()
           : base(HttpStatusCode.BadRequest, $"InValid forget password code", "invalid_forget_password_code")
        {
        }
    }
}

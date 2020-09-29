using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Brimo.IDP.STS.Identity.Common.Exceptions.Auth
{
    public class ChangePasswordException : BusinessException
    {
        public ChangePasswordException(string errorMessage, string errorCode)
           : base(HttpStatusCode.BadRequest, errorMessage, errorCode)
        {
        }
    }
}

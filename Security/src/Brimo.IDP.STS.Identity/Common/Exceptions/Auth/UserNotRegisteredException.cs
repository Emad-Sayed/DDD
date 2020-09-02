using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Brimo.IDP.STS.Identity.Common.Exceptions.Auth
{
    public class UserNotRegisteredException : BusinessException
    {
        public UserNotRegisteredException(string phoneNumber)
           : base(HttpStatusCode.BadRequest, $"User with phone number {phoneNumber} not registered", "user_not_registered")
        {
        }
    }
}

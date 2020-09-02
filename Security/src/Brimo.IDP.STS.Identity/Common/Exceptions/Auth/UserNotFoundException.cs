using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Brimo.IDP.STS.Identity.Common.Exceptions.Auth
{
    public class UserNotFoundException : BusinessException
    {
        public UserNotFoundException(string phoneNumber)
           : base(HttpStatusCode.NotFound, $"User with phone number {phoneNumber} not found", "user_not_found")
        {
        }
    }
}

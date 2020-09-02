using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Brimo.IDP.STS.Identity.Common.Exceptions.Auth
{
    public class UserAlreadyExistException : BusinessException
    {
        public UserAlreadyExistException(string phoneNumber)
           : base(HttpStatusCode.BadRequest, $"User with phone number {phoneNumber} already exist", "user_already_exist")
        {
        }
    }
}

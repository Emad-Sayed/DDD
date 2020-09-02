using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Brimo.IDP.STS.Identity.Common.Exceptions.Auth
{
    public class PhoneNumberNotConfirmedException : BusinessException
    {
        public PhoneNumberNotConfirmedException(string phoneNumber)
           : base(HttpStatusCode.BadRequest, $"Phone number {phoneNumber} not confirmed", "phone_number_not_confirmed")
        {
        }
    }
}

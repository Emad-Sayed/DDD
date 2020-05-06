using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Exceptions
{
    public class NotAuthorizedException : ValidationExceptionBase
    {

        public override string ErrorTemplate => "SE_{0}";

        public NotAuthorizedException(string notAuthorizedType) : base(new[] { notAuthorizedType })
        {
        }
    }
}

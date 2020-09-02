using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Brimo.IDP.STS.Identity.Common.Exceptions
{
    public class BusinessException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string ErrorCode { get; }

        public BusinessException(HttpStatusCode code, string message, string errorCode) : base(message)
        {
            StatusCode = code;
            ErrorCode = errorCode;
        }
    }
}

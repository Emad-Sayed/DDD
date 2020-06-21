using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.Common.Exceptions
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

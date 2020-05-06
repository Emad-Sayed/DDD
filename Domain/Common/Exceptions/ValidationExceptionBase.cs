using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Exceptions
{
    public abstract class ValidationExceptionBase : Exception
    {
        protected StringBuilder Code { get; }

        public string ErrorCode => $"[\"{Code}\"]";

        public abstract string ErrorTemplate { get; }

        protected ValidationExceptionBase(string[] properties)
        {
            Code = new StringBuilder("ERROR_");

            if (ErrorTemplate != null && properties != null) this.Code.Append(string.Format(ErrorTemplate, properties));
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Exceptions
{
    public class UniqueConstraintViolationException : ValidationExceptionBase
    {

        public override string ErrorTemplate => "BE_Exists_{0}_{1}";

        public UniqueConstraintViolationException(string entityName, string propertyName) : base(new[] { entityName, propertyName })
        {
        }
    }
}

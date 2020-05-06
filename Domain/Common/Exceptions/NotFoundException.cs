using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Exceptions
{
    public class NotFoundException : ValidationExceptionBase
    {
        public override string ErrorTemplate => "BE_NotFound_{0}";

        public NotFoundException(string entityName) : base(new[] { entityName })
        {
        }
    }
}

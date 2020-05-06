using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Exceptions
{
    public class DependencyViolationException : ValidationExceptionBase
    {
        public override string ErrorTemplate => "BE_{0}_{1}";

        public DependencyViolationException(string entityName, string dependantName) : base(new[] { entityName, dependantName })
        {
        }
    }
}

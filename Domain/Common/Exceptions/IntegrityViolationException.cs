using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Exceptions
{
    public class IntegrityViolationException : ValidationExceptionBase
    {
        public override string ErrorTemplate => "BE_IntegrityViolation_{0}_{1}";

        public IntegrityViolationException(string firstEntityName, string secondEntityName) : base(new[] { firstEntityName, secondEntityName })
        {

        }


    }
}

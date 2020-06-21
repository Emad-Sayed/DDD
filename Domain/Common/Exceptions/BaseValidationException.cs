using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Common.Exceptions
{
    public class BaseValidationException : Exception
    {
        public BaseValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string>();
        }

        public BaseValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            var failureGroups = failures
                .Select(e => new { ErrorCode = e.ErrorCode, ErrorMessage = e.ErrorMessage });

            foreach (var failureGroup in failureGroups)
            {
                var errorCode = failureGroup.ErrorCode;
                string errorMessage = failureGroup.ErrorMessage;

                Errors.Add(errorCode, errorMessage);
            }
        }

        public IDictionary<string, string> Errors { get; }

    }
}

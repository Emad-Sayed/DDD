using Domain.Common.Exceptions;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Validation
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new System.ComponentModel.DataAnnotations.ValidationContext(request);
            var failures = _validators.Select(x => x.Validate(context))
                                      .SelectMany(x => x.Errors)
                                      .Where(x => x != null)
                                      .Select(x => new DataTypeException.DataTypeExceptionError
                                      {
                                          PropertyName = x.PropertyName,
                                          Code = x.ErrorCode
                                      })
                                      .ToList();

            if (failures.Any())
                throw new DataTypeException(failures.ToArray());

            return next();
        }



    }
}

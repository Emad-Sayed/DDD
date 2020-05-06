using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.TransactionManagement
{
    public class TransactionMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly UnitOfWork _unitOfWork;


        public TransactionMiddleware(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var requestType = typeof(TRequest);

            if (!requestType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ITransactionalRequest<>)))
                return await next();

            _unitOfWork.StartWork();

            var result = await next();

            _unitOfWork.FinishWork();

            return result;
        }
    }
}

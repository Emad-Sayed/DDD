using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Application.Common.TransactionManagement
{
    public interface ITransactionalRequest<out TResponse> : IRequest<TResponse>
    {

    }
}

using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using Domain.DistributorManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.Commands.ConfirmDistributorUserEmail
{
    public class ConfirmDistributorUserEmailCommand : IRequest
    {
        public string AccountId { get; set; }

        public class Handler : IRequestHandler<ConfirmDistributorUserEmailCommand>
        {
            private readonly IDistributorRepository _distributorRepository;

            public Handler(IDistributorRepository distributorRepository)
            {
                _distributorRepository = distributorRepository;
            }

            public async Task<Unit> Handle(ConfirmDistributorUserEmailCommand request, CancellationToken cancellationToken)
            {
               
                await _distributorRepository.ConfirmDistributorUserEmail(request.AccountId);

                await _distributorRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}

using Domain.Common.Exceptions;
using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using Domain.DistributorManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.Commands.UpdateDistributorUser
{
    public class UpdateDistributorUserCommand : IRequest
    {
        public string DistributorId { get; set; }
        public string DistributorUserId { get; set; }
        public string FullName { get; set; }

        public class Handler : IRequestHandler<UpdateDistributorUserCommand>
        {
            private readonly IDistributorRepository _distributorRepository;

            public Handler(IDistributorRepository distributorRepository)
            {
                _distributorRepository = distributorRepository;
            }

            public async Task<Unit> Handle(UpdateDistributorUserCommand request, CancellationToken cancellationToken)
            {
                var distributorFromRepo = await _distributorRepository.FindByIdAsync(request.DistributorId);
                if (distributorFromRepo == null) throw new DistributorNotFoundException(request.DistributorId);


                distributorFromRepo.UpdateDistributorUser(request.DistributorUserId, request.DistributorId);

                _distributorRepository.Update(distributorFromRepo);

                await _distributorRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}

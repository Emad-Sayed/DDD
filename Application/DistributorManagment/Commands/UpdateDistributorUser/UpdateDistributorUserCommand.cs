using Application.Common.Exceptions;
using Domain.Common.Exceptions;
using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
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
                if (distributorFromRepo == null) if (distributorFromRepo == null) throw new RestException(System.Net.HttpStatusCode.BadRequest, new { Distributor = $"Distributor with id {request.DistributorId} not found ", code = "distributor_notfound" });


                distributorFromRepo.UpdateDistributorUser(request.DistributorUserId, request.DistributorId);

                _distributorRepository.Update(distributorFromRepo);

                await _distributorRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}

using Application.Common.Exceptions;
using Domain.Common.Exceptions;
using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using Domain.SharedKernel.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.Commands.UpdateDistributor
{
    public class UpdateDistributorCommand : IRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Area { get; set; }

        public class Handler : IRequestHandler<UpdateDistributorCommand>
        {
            private readonly IDistributorRepository _distributorRepository;

            public Handler(IDistributorRepository distributorRepository)
            {
                _distributorRepository = distributorRepository;
            }

            public async Task<Unit> Handle(UpdateDistributorCommand request, CancellationToken cancellationToken)
            {
                var distributorFromRepo = await _distributorRepository.FindByIdAsync(request.Id);
                if (distributorFromRepo == null) if (distributorFromRepo == null) throw new RestException(System.Net.HttpStatusCode.BadRequest, new { Distributor = $"Distributor with id {request.Id} not found ", code = "distributor_notfound" });


                distributorFromRepo.UpdateDistributor(request.Name, request.City, request.Area);

                _distributorRepository.Update(distributorFromRepo);

                await _distributorRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}

using Application.Common.Exceptions;
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
        public Address Address { get; set; }

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
                if (distributorFromRepo == null) throw new NotFoundException(nameof(distributorFromRepo));

                distributorFromRepo.UpdateDistributor(request.Name, request.Address.City, request.Address.Area);

                _distributorRepository.Update(distributorFromRepo);

                await _distributorRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}

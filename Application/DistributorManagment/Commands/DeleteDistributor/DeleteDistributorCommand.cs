using Domain.Common.Exceptions;
using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using Domain.DistributorManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.Commands.DeleteDistributor
{
    public class DeleteDistributorCommand : IRequest
    {

        public string DistributorId { get; set; }


        public class Handler : IRequestHandler<DeleteDistributorCommand>
        {
            private readonly IDistributorRepository _distributorRepository;

            public Handler(IDistributorRepository distributorRepository)
            {
                _distributorRepository = distributorRepository;
            }

            public async Task<Unit> Handle(DeleteDistributorCommand request, CancellationToken cancellationToken)
            {
                // get distributor by id
                var distributorFromRepo = await _distributorRepository.FindByIdAsync(request.DistributorId);
                if (distributorFromRepo == null) throw new DistributorNotFoundException(request.DistributorId);


                // update distributor with the  deleted
                _distributorRepository.Delete(distributorFromRepo);

                // save changes in the database and rase DistributorUpdated event
                await _distributorRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return MediatR.Unit.Value;
            }
        }
    }
}

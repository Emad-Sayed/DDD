using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.Commands.CreateDistributor
{
    public class CreateDistributorCommand : IRequest<string>
    {
        public string AccountId { get; set; }
        public string FullName { get; set; }
        public string LocationOnMap { get; set; }

        public class Handler : IRequestHandler<CreateDistributorCommand, string>
        {
            private readonly IDistributorRepository _distributorRepository;

            private readonly IMediator _mediator;

            public Handler(IDistributorRepository distributorRepository, IMediator mediator)
            {
                _distributorRepository = distributorRepository;
                _mediator = mediator;
            }

            public async Task<string> Handle(CreateDistributorCommand request, CancellationToken cancellationToken)
            {
                var entity = new Distributor(request.AccountId, request.FullName, request.LocationOnMap);

                _distributorRepository.Add(entity);

                await _distributorRepository.UnitOfWork.SaveEntitiesAsync();

                return entity.Id.ToString();
            }
        }
    }
}

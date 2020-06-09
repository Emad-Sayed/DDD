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
        public string Name { get; set; }
        public string City { get; set; }
        public string Region { get; set; }

        public class Handler : IRequestHandler<CreateDistributorCommand, string>
        {
            private readonly IDistributorRepository _distributorRepository;


            public Handler(IDistributorRepository distributorRepository)
            {
                _distributorRepository = distributorRepository;
            }

            public async Task<string> Handle(CreateDistributorCommand request, CancellationToken cancellationToken)
            {
                var entity = new Distributor(request.Name, request.City, request.Region);

                _distributorRepository.Add(entity);

                await _distributorRepository.UnitOfWork.SaveEntitiesAsync();

                return entity.Id.ToString();
            }
        }
    }
}

using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using Domain.DistributorManagment.Exceptions;
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
        public List<string> AreasIds { get; set; }

        public class Handler : IRequestHandler<CreateDistributorCommand, string>
        {
            private readonly IDistributorRepository _distributorRepository;


            public Handler(IDistributorRepository distributorRepository)
            {
                _distributorRepository = distributorRepository;
            }

            public async Task<string> Handle(CreateDistributorCommand request, CancellationToken cancellationToken)
            {
                var distributor = new Distributor(request.Name);

                foreach (var areaId in request.AreasIds)
                {
                    var area = await _distributorRepository.FindAreaById(areaId);
                    if (area == null) throw new AreaNotFoundException(areaId);
                    distributor.AddArea(area);
                }

                _distributorRepository.Add(distributor);

                await _distributorRepository.UnitOfWork.SaveEntitiesAsync();

                return distributor.Id.ToString();
            }
        }
    }
}

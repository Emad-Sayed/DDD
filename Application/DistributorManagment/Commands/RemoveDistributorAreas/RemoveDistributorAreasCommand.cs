using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using Domain.DistributorManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.Commands.RemoveDistributorAreas
{
    public class RemoveDistributorAreasCommand : IRequest
    {
        public string DistributorId { get; set; }
        public List<string> AreasIds { get; set; }

        public class Handler : IRequestHandler<RemoveDistributorAreasCommand>
        {
            private readonly IDistributorRepository _distributorRepository;

            public Handler(IDistributorRepository distributorRepository)
            {
                _distributorRepository = distributorRepository;
            }

            public async Task<Unit> Handle(RemoveDistributorAreasCommand request, CancellationToken cancellationToken)
            {
                var distributorFromRepo = await _distributorRepository.FindByIdAsync(request.DistributorId);
                if (distributorFromRepo == null) throw new DistributorNotFoundException(request.DistributorId);


                foreach (var areaId in request.AreasIds)
                {
                    var area = await _distributorRepository.FindAreaById(areaId);
                    if (area == null) throw new AreaNotFoundException(areaId);
                    distributorFromRepo.RemoveArea(area);
                }

                _distributorRepository.Update(distributorFromRepo);

                await _distributorRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}

using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using Domain.DistributorManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.Commands.DeleteArea
{
    public class DeleteAreaCommand : IRequest
    {
        public string CityId { get; set; }
        public string AreaId { get; set; }

        public class Handler : IRequestHandler<DeleteAreaCommand>
        {
            private readonly IDistributorRepository _distributorRepository;

            public Handler(IDistributorRepository distributorRepository)
            {
                _distributorRepository = distributorRepository;
            }

            public async Task<Unit> Handle(DeleteAreaCommand request, CancellationToken cancellationToken)
            {
                // get distributor by id
                var city = await _distributorRepository.FindCityByIdAsync(request.CityId);
                if (city == null) throw new CityNotFoundException(request.CityId);

                city.DeleteArea(request.AreaId);

                _distributorRepository.UpdateCity(city);

                // save changes in the database and rase DistributorUpdated event
                await _distributorRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}

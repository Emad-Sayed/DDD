using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using Domain.DistributorManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.Commands.DeleteCity
{
    public class DeleteCityCommand : IRequest
    {
        public string CityId { get; set; }

        public class Handler : IRequestHandler<DeleteCityCommand>
        {
            private readonly IDistributorRepository _distributorRepository;

            public Handler(IDistributorRepository distributorRepository)
            {
                _distributorRepository = distributorRepository;
            }

            public async Task<Unit> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
            {
                // get distributor by id
                var city = await _distributorRepository.FindCityByIdAsync(request.CityId);
                if (city == null) throw new CityNotFoundException(request.CityId);

                city.DeleteCity();

                _distributorRepository.DeleteCity(city);

                // save changes in the database and rase DistributorUpdated event
                await _distributorRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}

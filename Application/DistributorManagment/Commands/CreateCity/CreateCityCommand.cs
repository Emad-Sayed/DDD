using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using Domain.DistributorManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.Commands.CreateCity
{
    public class CreateCityCommand : IRequest<string>
    {
        public string Name { get; set; }


        public class Handler : IRequestHandler<CreateCityCommand, string>
        {
            private readonly IDistributorRepository _distributorRepository;


            public Handler(IDistributorRepository distributorRepository)
            {
                _distributorRepository = distributorRepository;
            }

            public async Task<string> Handle(CreateCityCommand request, CancellationToken cancellationToken)
            {
                var cityExist = await _distributorRepository.CityExistAsync(request.Name);
                if (cityExist) throw new CityAlreadyExistException(request.Name);

                var city = new City(Guid.NewGuid().ToString(), request.Name);

                _distributorRepository.AddCity(city);

                await _distributorRepository.UnitOfWork.SaveEntitiesAsync();

                return city.Id.ToString();
            }
        }
    }
}

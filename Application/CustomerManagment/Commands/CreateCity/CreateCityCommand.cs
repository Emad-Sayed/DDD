using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using Domain.CustomerManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CustomerManagment.Commands.CreateCity
{
    public class CreateCityCommand : IRequest<string>
    {
        public string Name { get; set; }


        public class Handler : IRequestHandler<CreateCityCommand, string>
        {
            private readonly ICustomerRepository _customerRepository;


            public Handler(ICustomerRepository customerRepository)
            {
                _customerRepository = customerRepository;
            }

            public async Task<string> Handle(CreateCityCommand request, CancellationToken cancellationToken)
            {
                var cityExist = await _customerRepository.CityExistAsync(request.Name);
                if (cityExist) throw new CityAlreadyExistException(request.Name);

                var city = new City(Guid.NewGuid().ToString(), request.Name);

                _customerRepository.AddCity(city);

                await _customerRepository.UnitOfWork.SaveEntitiesAsync();

                return city.Id.ToString();
            }
        }
    }
}

using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using Domain.CustomerManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CustomerManagment.Commands.UpdateCity
{
    public class UpdateCityCommand : IRequest<string>
    {
        public string CityId { get; set; }
        public string Name { get; set; }


        public class Handler : IRequestHandler<UpdateCityCommand, string>
        {
            private readonly ICustomerRepository _customerRepository;

            public Handler(ICustomerRepository customerRepository)
            {
                _customerRepository = customerRepository;
            }

            public async Task<string> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
            {
                var city = await _customerRepository.FindCityByIdAsync(request.CityId);
                if (city == null) throw new CityNotFoundException(request.CityId);

                var cityExist = await _customerRepository.CityExistAsync(request.Name);
                if (cityExist) throw new CityAlreadyExistException(request.Name);

                city.UpdateCity(request.Name);

                _customerRepository.UpdateCity(city);

                await _customerRepository.UnitOfWork.SaveEntitiesAsync();

                return city.Id.ToString();
            }
        }
    }
}

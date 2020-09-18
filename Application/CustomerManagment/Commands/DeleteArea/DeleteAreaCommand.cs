using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using Domain.CustomerManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CustomerManagment.Commands.DeleteArea
{
    public class DeleteAreaCommand : IRequest
    {
        public string CityId { get; set; }
        public string AreaId { get; set; }

        public class Handler : IRequestHandler<DeleteAreaCommand>
        {
            private readonly ICustomerRepository _customerRepository;

            public Handler(ICustomerRepository customerRepository)
            {
                _customerRepository = customerRepository;
            }

            public async Task<Unit> Handle(DeleteAreaCommand request, CancellationToken cancellationToken)
            {
                // get customer by id
                var city = await _customerRepository.FindCityByIdAsync(request.CityId);
                if (city == null) throw new CityNotFoundException(request.CityId);

                city.DeleteArea(request.AreaId);

                _customerRepository.UpdateCity(city);

                // save changes in the database and rase CustomerUpdated event
                await _customerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}

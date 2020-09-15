using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using Domain.CustomerManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Application.CustomerManagment.Commands.CreateCustomer
{

    public class CreateCustomerCommand : IRequest<string>
    {
        public string AccountId { get; set; }
        public string FullName { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string LocationOnMap { get; set; }
        public string AreaId { get; set; }

        public class Handler : IRequestHandler<CreateCustomerCommand, string>
        {
            private readonly ICustomerRepository _customerRepository;

            private readonly IMediator _mediator;

            public Handler(ICustomerRepository customerRepository, IMediator mediator)
            {
                _customerRepository = customerRepository;
                _mediator = mediator;
            }

            public async Task<string> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                var customer = new Customer
                    (
                    request.AccountId,
                    request.FullName,
                    request.ShopName,
                    request.ShopAddress,
                    request.LocationOnMap
                    );

                var area = await _customerRepository.FindAreaById(request.AreaId);
                if (area == null) throw new AreaNotFoundException(request.AreaId);

                customer.AddArea(area);

                _customerRepository.Add(customer);

                await _customerRepository.UnitOfWork.SaveEntitiesAsync();

                return customer.Id.ToString();
            }
        }
    }

}

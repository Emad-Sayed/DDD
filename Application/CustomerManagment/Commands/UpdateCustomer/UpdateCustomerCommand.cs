using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using MediatR;

namespace Application.CustomerManagment.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<string>
    {
        public string AccountId { get; set; }
        public string FullName { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string LocationOnMap { get; set; }
        public string City { get; set; }
        public string Area { get; set; }

        public class Handler : IRequestHandler<UpdateCustomerCommand, string>
        {
            private readonly ICustomerRepository _customerRepository;

            private readonly IMediator _mediator;

            public Handler(ICustomerRepository customerRepository, IMediator mediator)
            {
                _customerRepository = customerRepository;
                _mediator = mediator;
            }

            public async Task<string> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
            {
                var customer = await _customerRepository.GetCustomerByAccountId(request.AccountId);
                customer.UpdateCustomer(
                    request.City,
                    request.Area,
                    request.FullName,
                    request.ShopName,
                    request.ShopAddress,
                    request.LocationOnMap
                );

                _customerRepository.Update(customer);

                await _customerRepository.UnitOfWork.SaveEntitiesAsync();

                return customer.Id.ToString();
            }
        }
    }
}

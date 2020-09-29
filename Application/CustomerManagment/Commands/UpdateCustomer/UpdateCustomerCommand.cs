using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using Domain.CustomerManagment.Exceptions;
using MediatR;

namespace Application.CustomerManagment.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<string>
    {
        public string AccountId { get; set; }
        public string FullName { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string AreaId { get; set; }
        public string LocationOnMap { get; set; }

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
                if (customer == null) throw new CustomerNotFoundException(request.AccountId);

                if(!string.IsNullOrEmpty(request.FullName))
                {
                    customer.UpdateFullName(request.FullName);
                }

                if (!string.IsNullOrEmpty(request.ShopName))
                {
                    customer.UpdateShopName(request.ShopName);
                }

                if (!string.IsNullOrEmpty(request.ShopAddress))
                {
                    customer.UpdateShopAddress(request.ShopAddress);
                }

                if (!string.IsNullOrEmpty(request.LocationOnMap))
                {
                    customer.UpdateLocationOnMap(request.LocationOnMap);
                }

                if (!string.IsNullOrEmpty(request.AreaId))
                {
                    var area = await _customerRepository.FindAreaById(request.AreaId);
                    if (area == null) throw new AreaNotFoundException(request.AreaId);

                    customer.ChangeArea(area);
                }

                _customerRepository.Update(customer);

                await _customerRepository.UnitOfWork.SaveEntitiesAsync();

                return customer.Id.ToString();
            }
        }
    }
}

using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
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
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string LocationOnMap { get; set; }

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
                var entity = new Customer
                    (
                    request.AccountId,
                    request.ShopName,
                    request.ShopAddress,
                    request.LocationOnMap
                    );

                _customerRepository.Add(entity);

                await _customerRepository.UnitOfWork.SaveEntitiesAsync();

                return entity.Id.ToString();
            }
        }
    }

}

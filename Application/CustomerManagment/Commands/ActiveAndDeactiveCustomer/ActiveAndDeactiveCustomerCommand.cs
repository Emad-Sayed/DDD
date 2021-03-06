using Domain.Common.Exceptions;
using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using Domain.CustomerManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CustomerManagment.Commands.DeleteCustomer
{
    public class ActiveAndDeactiveCustomerCommand : IRequest
    {
        public string CustomerId { get; set; }

        public class Handler : IRequestHandler<ActiveAndDeactiveCustomerCommand>
        {
            private readonly ICustomerRepository _customerRepository;


            public Handler(ICustomerRepository customerRepository)
            {
                _customerRepository = customerRepository;
            }

            public async Task<Unit> Handle(ActiveAndDeactiveCustomerCommand request, CancellationToken cancellationToken)
            {
                var customerFromRepo = await _customerRepository.FindByIdAsync(request.CustomerId);
                if (customerFromRepo == null) throw new CustomerNotFoundException(request.CustomerId);

                customerFromRepo.ActiveAndDeactiveCustomer();

                _customerRepository.Update(customerFromRepo);

                await _customerRepository.UnitOfWork.SaveEntitiesAsync();

                return Unit.Value;
            }
        }
    }
}

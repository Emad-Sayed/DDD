using Domain.Common.Exceptions;
using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CustomerManagment.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest
    {
        public string CustomerId { get; set; }

        public class Handler : IRequestHandler<DeleteCustomerCommand>
        {
            private readonly ICustomerRepository _customerRepository;


            public Handler(ICustomerRepository customerRepository)
            {
                _customerRepository = customerRepository;
            }

            public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
            {
                var customerFromRepo = await _customerRepository.FindByIdAsync(request.CustomerId);
                if (customerFromRepo == null) throw new RestException(HttpStatusCode.NotFound, new { Customer = $"Customer with id {request.CustomerId} not found ", code = "cusotmer_notfound" });

                customerFromRepo.Delete();

                _customerRepository.Delete(customerFromRepo);

                await _customerRepository.UnitOfWork.SaveEntitiesAsync();

                return Unit.Value;
            }
        }
    }
}

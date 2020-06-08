using Application.CustomerManagment.ViewModels;
using AutoMapper;
using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CustomerManagment.Queries.CustomerByAccountId
{
    public class CustomerByAccountIdQuery : IRequest<CustomerVM>
    {
        public string AccountId { get; set; }

        public class Handler : IRequestHandler<CustomerByAccountIdQuery, CustomerVM>
        {
            private readonly ICustomerRepository _customersRepository;
            private readonly IMapper _mapper;

            public Handler(ICustomerRepository customerRepository, IMapper mapper)
            {
                _customersRepository = customerRepository;
                _mapper = mapper;
            }

            public async Task<CustomerVM> Handle(CustomerByAccountIdQuery request, CancellationToken cancellationToken)
            {
                var customerFromRepo = await _customersRepository.GetCustomerByAccountId(request.AccountId);

                var customerToReturn = _mapper.Map<CustomerVM>(customerFromRepo);

                return customerToReturn;
            }
        }
    }
}

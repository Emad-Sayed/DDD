using Application.CustomerManagment.ViewModels;
using AutoMapper;
using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CustomerManagment.Queries.CustomerById
{
    public class CustomerByIdQuery : IRequest<CustomerVM>
    {
        public string CustomerId { get; set; }

        public class Handler : IRequestHandler<CustomerByIdQuery, CustomerVM>
        {
            private readonly ICustomerRepository _customersRepository;
            private readonly IMapper _mapper;

            public Handler(ICustomerRepository customerRepository, IMapper mapper)
            {
                _customersRepository = customerRepository;
                _mapper = mapper;
            }

            public async Task<CustomerVM> Handle(CustomerByIdQuery request, CancellationToken cancellationToken)
            {
                var customerFromRepo = await _customersRepository.FindByIdAsync(request.CustomerId);

                var customerToReturn = _mapper.Map<CustomerVM>(customerFromRepo);

                return customerToReturn;
            }
        }
    }

}

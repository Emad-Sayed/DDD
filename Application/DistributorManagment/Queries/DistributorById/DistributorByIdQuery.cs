using Application.DistributorManagment.ViewModels;
using AutoMapper;
using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.Queries.DistributorById
{
    public class DistributorByIdQuery : IRequest<DistributorVM>
    {
        public string DistributorId { get; set; }

        public class Handler : IRequestHandler<DistributorByIdQuery, DistributorVM>
        {
            private readonly IDistributorRepository _customersRepository;
            private readonly IMapper _mapper;

            public Handler(IDistributorRepository customerRepository, IMapper mapper)
            {
                _customersRepository = customerRepository;
                _mapper = mapper;
            }

            public async Task<DistributorVM> Handle(DistributorByIdQuery request, CancellationToken cancellationToken)
            {
                var customerFromRepo = await _customersRepository.FindByIdAsync(request.DistributorId);

                var customerToReturn = _mapper.Map<DistributorVM>(customerFromRepo);

                return customerToReturn;
            }
        }
    }
}

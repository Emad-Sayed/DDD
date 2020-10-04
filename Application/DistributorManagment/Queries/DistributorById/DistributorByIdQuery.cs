using Application.DistributorManagment.ViewModels;
using AutoMapper;
using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using Domain.DistributorManagment.Exceptions;
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
            private readonly IDistributorRepository _distributorsRepository;
            private readonly IMapper _mapper;

            public Handler(IDistributorRepository distributorRepository, IMapper mapper)
            {
                _distributorsRepository = distributorRepository;
                _mapper = mapper;
            }

            public async Task<DistributorVM> Handle(DistributorByIdQuery request, CancellationToken cancellationToken)
            {
                var distributorFromRepo = await _distributorsRepository.FindByIdAsync(request.DistributorId);
                if (distributorFromRepo == null) throw new DistributorNotFoundException(request.DistributorId);
                        
                var distributorToReturn = _mapper.Map<DistributorVM>(distributorFromRepo);

                return distributorToReturn;
            }
        }
    }
}

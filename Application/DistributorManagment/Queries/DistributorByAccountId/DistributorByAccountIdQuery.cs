using Application.DistributorManagment.ViewModels;
using AutoMapper;
using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.Queries.DistributorByAccountId
{
    public class DistributorByAccountIdQuery : IRequest<DistributorVM>
    {
        public string AccountId { get; set; }

        public class Handler : IRequestHandler<DistributorByAccountIdQuery, DistributorVM>
        {
            private readonly IDistributorRepository _distributorsRepository;
            private readonly IMapper _mapper;

            public Handler(IDistributorRepository distributorRepository, IMapper mapper)
            {
                _distributorsRepository = distributorRepository;
                _mapper = mapper;
            }

            public async Task<DistributorVM> Handle(DistributorByAccountIdQuery request, CancellationToken cancellationToken)
            {
                var distributorFromRepo = await _distributorsRepository.GetDistributorByAccountId(request.AccountId);

                var distributorToReturn = _mapper.Map<DistributorVM>(distributorFromRepo);

                return distributorToReturn;
            }
        }
    }
}

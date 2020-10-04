using Application.Common.Interfaces;
using Application.Common.Models;
using Application.CustomerManagment.Queries.CustomerByAccountId;
using Application.DistributorManagment.ViewModels;
using AutoMapper;
using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.Queries.ListDistributorsInMyArea
{
    public class ListDistributorsInMyAreaQuery : IRequest<List<DistributorVM>>
    {

        public class Handler : IRequestHandler<ListDistributorsInMyAreaQuery, List<DistributorVM>>
        {
            private readonly ICurrentUserService _currentUserService;
            private readonly IDistributorRepository _distributorsRepository;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public Handler(IDistributorRepository brandRepository, IMapper mapper, ICurrentUserService currentUserService, IMediator mediator)
            {
                _distributorsRepository = brandRepository;
                _mapper = mapper;
                _currentUserService = currentUserService;
                _mediator = mediator;
            }

            public async Task<List<DistributorVM>> Handle(ListDistributorsInMyAreaQuery request, CancellationToken cancellationToken)
            {
                // Get Current Customer form the current logged in customer
                var customer = await _mediator.Send(new CustomerByAccountIdQuery { AccountId = _currentUserService.UserId }, cancellationToken);


                // get distributors paginated form the database 
                var distributorsFromRepo = await _distributorsRepository.GetDistributorsInAreaAsync(customer.Area.Id);

                // mapping distributors to cusotmers view models
                var distributorsToReturn = _mapper.Map<List<DistributorVM>>(distributorsFromRepo);

                return distributorsToReturn;
            }
        }
    }
}

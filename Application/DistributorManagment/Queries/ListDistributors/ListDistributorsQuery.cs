using Application.Common.Models;
using Application.DistributorManagment.ViewModels;
using AutoMapper;
using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.Queries.ListDistributors
{
    public class ListDistributorsQuery : IRequest<ListEntityVM<DistributorVM>>
    {
        // pagination parameters
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string KeyWord { get; set; }

        public class Handler : IRequestHandler<ListDistributorsQuery, ListEntityVM<DistributorVM>>
        {
            private readonly IDistributorRepository _distributorsRepository;
            private readonly IMapper _mapper;

            public Handler(IDistributorRepository brandRepository, IMapper mapper)
            {
                _distributorsRepository = brandRepository;
                _mapper = mapper;
            }

            public async Task<ListEntityVM<DistributorVM>> Handle(ListDistributorsQuery request, CancellationToken cancellationToken)
            {
                // get distributors paginated form the database 
                var distributorsFromRepo = await _distributorsRepository.GetAllAsync(request.PageNumber, request.PageSize, request.KeyWord);

                // mapping distributors to cusotmers view models
                var distributorsToReturn = _mapper.Map<List<DistributorVM>>(distributorsFromRepo.Item2);

                return new ListEntityVM<DistributorVM> { TotalCount = distributorsFromRepo.Item1, Data = distributorsToReturn };
            }
        }
    }
}

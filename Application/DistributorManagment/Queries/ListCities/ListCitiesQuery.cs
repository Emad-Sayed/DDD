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

namespace Application.DistributorManagment.Queries.ListCities
{
    public class ListCitiesQuery : IRequest<ListEntityVM<CityVM>>
    {
        // pagination parameters
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 1000;
        public string KeyWord { get; set; }

        public class Handler : IRequestHandler<ListCitiesQuery, ListEntityVM<CityVM>>
        {
            private readonly IDistributorRepository _distributorsRepository;
            private readonly IMapper _mapper;

            public Handler(IDistributorRepository distributorRepository, IMapper mapper)
            {
                _distributorsRepository = distributorRepository;
                _mapper = mapper;
            }

            public async Task<ListEntityVM<CityVM>> Handle(ListCitiesQuery request, CancellationToken cancellationToken)
            {
                // get citys paginated form the database 
                var citysFromRepo = await _distributorsRepository.GetAllCitiesAsync(request.PageNumber, request.PageSize, request.KeyWord);

                // mapping citys to cusotmers view models
                var citysToReturn = _mapper.Map<List<CityVM>>(citysFromRepo.Item2);

                return new ListEntityVM<CityVM> { TotalCount = citysFromRepo.Item1, Data = citysToReturn };
            }
        }
    }
}

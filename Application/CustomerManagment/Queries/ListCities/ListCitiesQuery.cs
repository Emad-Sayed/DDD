using Application.Common.Models;
using Application.CustomerManagment.ViewModels;
using AutoMapper;
using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CustomerManagment.Queries.ListCities
{
    public class ListCitiesQuery : IRequest<ListEntityVM<CityVM>>
    {
        // pagination parameters
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 1000;
        public string KeyWord { get; set; }

        public class Handler : IRequestHandler<ListCitiesQuery, ListEntityVM<CityVM>>
        {
            private readonly ICustomerRepository _customersRepository;
            private readonly IMapper _mapper;

            public Handler(ICustomerRepository brandRepository, IMapper mapper)
            {
                _customersRepository = brandRepository;
                _mapper = mapper;
            }

            public async Task<ListEntityVM<CityVM>> Handle(ListCitiesQuery request, CancellationToken cancellationToken)
            {
                // get citys paginated form the database 
                var citysFromRepo = await _customersRepository.GetAllCitiesAsync(request.PageNumber, request.PageSize, request.KeyWord);

                // mapping citys to cusotmers view models
                var citysToReturn = _mapper.Map<List<CityVM>>(citysFromRepo.Item2);

                return new ListEntityVM<CityVM> { TotalCount = citysFromRepo.Item1, Data = citysToReturn };
            }
        }
    }
}

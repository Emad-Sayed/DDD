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

namespace Application.CustomerManagment.Queries.ListCustomers
{
    public class ListCustomersQuery : IRequest<ListEntityVM<CustomerVM>>
    {
        // pagination parameters
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string KeyWord { get; set; }

        public class Handler : IRequestHandler<ListCustomersQuery, ListEntityVM<CustomerVM>>
        {
            private readonly ICustomerRepository _customersRepository;
            private readonly IMapper _mapper;

            public Handler(ICustomerRepository brandRepository, IMapper mapper)
            {
                _customersRepository = brandRepository;
                _mapper = mapper;
            }

            public async Task<ListEntityVM<CustomerVM>> Handle(ListCustomersQuery request, CancellationToken cancellationToken)
            {
                // get customers paginated form the database 
                var customersFromRepo = await _customersRepository.GetAllAsync(request.PageNumber, request.PageSize, request.KeyWord);
               
                // mapping customers to cusotmers view models
                var customersToReturn = _mapper.Map<List<CustomerVM>>(customersFromRepo.Item2);

                return new ListEntityVM<CustomerVM> { TotalCount = customersFromRepo.Item1, Data = customersToReturn };
            }
        }
    }
}

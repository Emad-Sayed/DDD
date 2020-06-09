using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OrderManagment.ViewModels;
using AutoMapper;
using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderManagment.Queries.CustomerOrders
{
    public class CustomerOrdersQuery : IRequest<ListEntityVM<OrderVM>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string KeyWord { get; set; }
        public List<OrderStatus> OrderStatuses { get; set; }

        public class Handler : IRequestHandler<CustomerOrdersQuery, ListEntityVM<OrderVM>>
        {
            private readonly IOrderRepository _ordersRepository;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;

            public Handler(IOrderRepository brandRepository, IMapper mapper, ICurrentUserService currentUserService)
            {
                _ordersRepository = brandRepository;
                _mapper = mapper;
                _currentUserService = currentUserService;
            }

            public async Task<ListEntityVM<OrderVM>> Handle(CustomerOrdersQuery request, CancellationToken cancellationToken)
            {
                var customerOrdersFromRepo = await _ordersRepository.GetCustomerOrders(_currentUserService.UserId, request.OrderStatuses, request.PageNumber, request.PageSize, request.KeyWord);

                var ordersToReturn = _mapper.Map<List<OrderVM>>(customerOrdersFromRepo.Item2);

                return new ListEntityVM<OrderVM> { TotalCount = customerOrdersFromRepo.Item1, Data = ordersToReturn };
            }
        }
    }
}

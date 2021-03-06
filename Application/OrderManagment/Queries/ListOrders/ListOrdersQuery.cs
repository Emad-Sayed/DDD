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

namespace Application.OrderManagment.Queries.ListOrders
{
    public class ListOrdersQuery : IRequest<ListEntityVM<OrderVM>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string KeyWord { get; set; }
        public List<OrderStatus> OrderStatuses { get; set; }
        public string DistributorId { get; set; }

        public class Handler : IRequestHandler<ListOrdersQuery, ListEntityVM<OrderVM>>
        {
            private readonly IOrderRepository _ordersRepository;
            private readonly IMapper _mapper;

            public Handler(IOrderRepository brandRepository, IMapper mapper)
            {
                _ordersRepository = brandRepository;
                _mapper = mapper;
            }

            public async Task<ListEntityVM<OrderVM>> Handle(ListOrdersQuery request, CancellationToken cancellationToken)
            {
                var ordersFromRepo = await _ordersRepository.GetAllAsync(request.OrderStatuses, request.PageNumber, request.PageSize, request.KeyWord, request.DistributorId);

                var ordersToReturn = _mapper.Map<List<OrderVM>>(ordersFromRepo.Item2);

                return new ListEntityVM<OrderVM> { TotalCount = ordersFromRepo.Item1, Data = ordersToReturn };
            }
        }
    }
}

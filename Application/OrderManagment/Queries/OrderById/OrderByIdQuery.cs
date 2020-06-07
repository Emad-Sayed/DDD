using Application.OrderManagment.ViewModels;
using AutoMapper;
using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderManagment.Queries.OrderById
{
    public class OrderByIdQuery : IRequest<OrderVM>
    {
        public string OrderId { get; set; }

        public class Handler : IRequestHandler<OrderByIdQuery, OrderVM>
        {
            private readonly IOrderRepository _ordersRepository;
            private readonly IMapper _mapper;

            public Handler(IOrderRepository brandRepository, IMapper mapper)
            {
                _ordersRepository = brandRepository;
                _mapper = mapper;
            }

            public async Task<OrderVM> Handle(OrderByIdQuery request, CancellationToken cancellationToken)
            {
                var orderFromRepo = await _ordersRepository.GetByIdAsync(request.OrderId);

                var orderToReturn = _mapper.Map<OrderVM>(orderFromRepo);

                return orderToReturn;
            }
        }
    }
}

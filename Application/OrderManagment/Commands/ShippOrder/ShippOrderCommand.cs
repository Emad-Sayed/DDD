using Application.Common.Interfaces;
using Domain.Common.Exceptions;
using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using Domain.OrderManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderManagment.Commands.ShippOrder
{
    public class ShippOrderCommand : IRequest
    {
        public string OrderId { get; set; }

        public class Handler : IRequestHandler<ShippOrderCommand>
        {
            private readonly IOrderRepository _orderRepository;

            public Handler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<Unit> Handle(ShippOrderCommand request, CancellationToken cancellationToken)
            {
                var orderToShipp = await _orderRepository.GetByIdAsync(request.OrderId);
                if (orderToShipp == null) throw new RestException(HttpStatusCode.NotFound, new { Order = $"Order with id {request.OrderId} not found ", code = "order_notfound" });

                orderToShipp.ShippOrder();

                await _orderRepository.UnitOfWork.SaveEntitiesAsync();

                return Unit.Value;
            }
        }
    }
}

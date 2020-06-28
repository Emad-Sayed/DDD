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

namespace Application.OrderManagment.Commands.DeliverOrder
{
    public class DeliverOrderCommand : IRequest
    {
        public string OrderId { get; set; }

        public class Handler : IRequestHandler<DeliverOrderCommand>
        {
            private readonly IOrderRepository _orderRepository;

            public Handler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<Unit> Handle(DeliverOrderCommand request, CancellationToken cancellationToken)
            {
                var orderToDeliver = await _orderRepository.GetByIdAsync(request.OrderId);
                if (orderToDeliver == null) throw new OrderNotFoundException(request.OrderId);

                if (orderToDeliver.OrderStatus != OrderStatus.Shipped) throw new OrderNotShippedException(request.OrderId);

                orderToDeliver.DeliverOrder();

                await _orderRepository.UnitOfWork.SaveEntitiesAsync();

                return Unit.Value;
            }
        }
    }
}

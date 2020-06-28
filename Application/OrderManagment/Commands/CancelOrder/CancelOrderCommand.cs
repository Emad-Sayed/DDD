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

namespace Application.OrderManagment.Commands.CancelOrder
{
    public class CancelOrderCommand : IRequest
    {
        public string OrderId { get; set; }
        public class Handler : IRequestHandler<CancelOrderCommand>
        {
            private readonly IOrderRepository _orderRepository;

            public Handler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
            {
                var orderToCancel = await _orderRepository.GetByIdAsync(request.OrderId);
                if (orderToCancel == null) throw new OrderNotFoundException(request.OrderId);

                if (orderToCancel.OrderStatus != OrderStatus.Placed) throw new CancelConfirmedOrderException(request.OrderId);

                orderToCancel.CancelOrder();

                await _orderRepository.UnitOfWork.SaveEntitiesAsync();

                return Unit.Value;
            }
        }
    }
}

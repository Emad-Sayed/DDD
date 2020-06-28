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

namespace Application.OrderManagment.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest
    {
        public string OrderId { get; set; }
        public string OrderItemId { get; set; }
        public string UnitName { get; set; }
        public string UnitId { get; set; }
        public float UnitPrice { get; set; }
        public int UnitCount { get; set; }

        public class Handler : IRequestHandler<UpdateOrderCommand>
        {
            private readonly IOrderRepository _orderRepository;

            public Handler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
            {
                var orderFromRepo = await _orderRepository.GetByIdAsync(request.OrderId);
                if (orderFromRepo == null) throw new OrderNotFoundException(request.OrderId);
                
                orderFromRepo.UpdateOrderItem(request.OrderItemId, request.UnitId, request.UnitName, request.UnitPrice, request.UnitCount);

                _orderRepository.Update(orderFromRepo);

                await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}

using Application.Common.Interfaces;
using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderManagment.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest
    {
        public class Handler : IRequestHandler<UpdateOrderCommand>
        {
            private readonly ICurrentUserService _currentUserService;
            private readonly IOrderRepository _orderRepository;

            public Handler(ICurrentUserService currentUserService, IOrderRepository orderRepository)
            {
                _currentUserService = currentUserService;
                _orderRepository = orderRepository;
            }

            public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
            {

                return Unit.Value;
            }
        }
    }
}

﻿using Application.Common.Interfaces;
using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using Domain.OrderManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderManagment.Commands.ConfirmOrder
{
    public class ConfirmOrderCommand : IRequest
    {
        public string OrderId { get; set; }

        public class Handler : IRequestHandler<ConfirmOrderCommand>
        {
            private readonly IOrderRepository _orderRepository;

            public Handler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<Unit> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
            {
                var orderToConfirm = await _orderRepository.GetByIdAsync(request.OrderId);
                if (orderToConfirm == null) throw new OrderingDomainException("order_not_found");

                orderToConfirm.ConfirmOrder();

                await _orderRepository.UnitOfWork.SaveEntitiesAsync();

                return Unit.Value;
            }
        }
    }
}

﻿using Application.Common.Interfaces;
using Application.CustomerManagment.Queries.CustomerByAccountId;
using Application.OrderManagment.ViewModels;
using Application.ShoppingVanBoundedContext.ShoppingVanAggregate.Queries.CurrentCustomerVan;
using AutoMapper;
using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using Domain.OrderManagment.Exceptions;
using MediatR;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderManagment.Commands.PlaceOrder
{
    public class PlaceOrderCommand : IRequest
    {
        public string Address { get; set; }
        public class Handler : IRequestHandler<PlaceOrderCommand>
        {
            private readonly IMediator _mediator;
            private readonly ICurrentUserService _currentUserService;
            private readonly IOrderRepository _orderRepository;

            public Handler(
                IMediator mediator,
                ICurrentUserService currentUserService,
                IOrderRepository orderRepository)
            {
                _mediator = mediator;
                _currentUserService = currentUserService;
                _orderRepository = orderRepository;
            }

            public async Task<Unit> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
            {
                // Get Current Open shopping van for the current logged in customer
                var customerVanFromQuery = await _mediator.Send(new CurrentCustomerVanQuery { }, cancellationToken);
                if (customerVanFromQuery == null) throw new OrderingDomainException("customer_shopping_van_can_notbe_empty");

                // Get Current Customer form the current logged in customer
                var customerDetailsFromQuery = await _mediator.Send(new CustomerByAccountIdQuery { AccountId = _currentUserService.UserId }, cancellationToken);
                if (customerDetailsFromQuery == null) throw new OrderingDomainException("customer_can_notbe_null");


                // If the Address sent then we will skip the customer address and make the order address equle this address
                string orderAddress = string.IsNullOrEmpty(request.Address) ? customerDetailsFromQuery.ShopAddress : request.Address;

                // create new order
                var orderToPlace = new Order(_currentUserService.UserId, customerDetailsFromQuery.ShopName, orderAddress);

                _orderRepository.Add(orderToPlace);
                await _orderRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync();

                foreach (var vanItem in customerVanFromQuery.ShoppingVanItems)
                {
                    orderToPlace.AddOrderItem(vanItem.ProductId, vanItem.ProductName, vanItem.UnitPrice, vanItem.PhotoUrl, vanItem.UnitId, vanItem.UnitName, vanItem.Amount);
                }

                _orderRepository.Update(orderToPlace);

                await _orderRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync();

                return Unit.Value;
            }
        }
    }
}
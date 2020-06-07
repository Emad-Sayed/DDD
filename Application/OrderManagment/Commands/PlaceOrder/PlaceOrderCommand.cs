using Application.Common.Interfaces;
using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using Domain.OrderManagment.Exceptions;
using Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate;
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
            private readonly IShoppingVanRepository _shoppingVanRepository;
            private readonly ICurrentUserService _currentUserService;
            private readonly IOrderRepository _orderRepository;

            public Handler(IShoppingVanRepository shoppingVanRepository, ICurrentUserService currentUserService, IOrderRepository orderRepository)
            {
                _shoppingVanRepository = shoppingVanRepository;
                _currentUserService = currentUserService;
                _orderRepository = orderRepository;
            }

            public async Task<Unit> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
            {
                // Get Current Open shopping van for the current logged in customer
                var customerVan = await _shoppingVanRepository.GetCustomerShoppingVan(_currentUserService.UserId);
                if (customerVan == null) throw new OrderingDomainException("customer_shopping_van_can_notbe_empty");

                // If the Address sent then we will skip the customer address and make the order address equle this address
                string orderAddress = string.IsNullOrEmpty(request.Address) ? _currentUserService.Address : request.Address;

                // create new order
                var orderToPlace = new Order(_currentUserService.UserId, "Customer name", orderAddress);

                _orderRepository.Add(orderToPlace);
                await _orderRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync();

                foreach (var product in customerVan.ShoppingVanItems)
                {
                    orderToPlace.AddOrderItem(product.Id.ToString(), product.ProductName, product.UnitPrice, product.PhotoUrl, product.UnitId, product.UnitName, product.Amount);
                }

                _orderRepository.Update(orderToPlace);

                await _orderRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync();

                return Unit.Value;
            }
        }
    }
}

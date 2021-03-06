using Application.Common.Interfaces;
using Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ShoppingVan.Commands.RemoveItemFromVan
{
    public class RemoveItemFromVanCommand : IRequest<int>
    {
        public string ProductId { get; set; }
        public string UnitId { get; set; }

        public class Handler : IRequestHandler<RemoveItemFromVanCommand, int>
        {
            private readonly IShoppingVanRepository _shoppingVanRepository;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IShoppingVanRepository shoppingVanRepository, ICurrentUserService currentUserService)
            {
                _shoppingVanRepository = shoppingVanRepository;
                _currentUserService = currentUserService;
            }

            public async Task<int> Handle(RemoveItemFromVanCommand request, CancellationToken cancellationToken)
            {
                // Get current logged in user shopping van
                var van = await _shoppingVanRepository.GetCustomerShoppingVan(_currentUserService.UserId);

                if (van == null)
                {
                    van = new Van(_currentUserService.UserId);
                }

                van.RemoveItem(request.ProductId, request.UnitId);

                _shoppingVanRepository.Update(van);

                await _shoppingVanRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return van.TotalItemsCount;
            }
        }
    }
}

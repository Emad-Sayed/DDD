using Application.Common.Interfaces;
using Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ShoppingVanBoundedContext.ShoppingVanAggregate.Commands.AddItemToVan
{
    public class AddItemToVanCommand : IRequest<int>
    {
        public string ProductId { get; set; }

        public class Handler : IRequestHandler<AddItemToVanCommand, int>
        {
            private readonly IShoppingVanRepository _shoppingVanRepository;
            private readonly ICurrentUserService _currentUserService;

            public Handler(IShoppingVanRepository shoppingVanRepository, ICurrentUserService currentUserService)
            {
                _shoppingVanRepository = shoppingVanRepository;
                _currentUserService = currentUserService;
            }

            public async Task<int> Handle(AddItemToVanCommand request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(_currentUserService.UserId))
                    throw new Exception("you must login");

                // Get current logged in user shopping van
                var van = await _shoppingVanRepository.GetCustomerShoppingVan(_currentUserService.UserId);

                if(van == null)
                {
                    van = new Van(_currentUserService.UserId);
                }

                van.AddItem(request.ProductId);

                _shoppingVanRepository.Update(van);

                await _shoppingVanRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return van.TotalItems;
            }
        }
    }
}

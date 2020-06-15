using Application.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ShoppingVan.Commands.AddItemToVan
{
    public class AddItemToVanCommand : IRequest<int>
    {
        public string UnitId { get; set; }
        public string ProductId { get; set; }

        public class Handler : IRequestHandler<AddItemToVanCommand, int>
        {
            private readonly IProductRepository _productRepository;
            private readonly IShoppingVanRepository _shoppingVanRepository;
            private readonly ICurrentUserService _currentUserService;

            public Handler(
                IShoppingVanRepository shoppingVanRepository,
                IProductRepository productRepository,
                ICurrentUserService currentUserService)
            {
                _shoppingVanRepository = shoppingVanRepository;
                _productRepository = productRepository;
                _currentUserService = currentUserService;
            }

            public async Task<int> Handle(AddItemToVanCommand request, CancellationToken cancellationToken)
            {
                // Get current logged in user shopping van
                var van = await _shoppingVanRepository.GetCustomerShoppingVan(_currentUserService.UserId);

                if (van == null)
                {
                    van = new Van(_currentUserService.UserId);
                    _shoppingVanRepository.Add(van);
                    await _shoppingVanRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);
                }

                var productToAddToVan = await _productRepository.FindByIdAsync(request.ProductId);

                // selected product unit
                var selectedUnit = productToAddToVan.Units.FirstOrDefault(x => x.Id == new Guid(request.UnitId));
                
                // Adding product to van
                van.AddItem(request.ProductId, productToAddToVan.Name, selectedUnit.Id.ToString(), selectedUnit.Name, selectedUnit.Price, productToAddToVan.PhotoUrl, selectedUnit.SellingPrice);

                _shoppingVanRepository.Update(van);

                await _shoppingVanRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return van.TotalItemsCount;
            }
        }
    }
}

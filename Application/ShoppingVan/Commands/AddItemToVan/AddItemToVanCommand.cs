using Application.Common.Interfaces;
using Application.ProductCatalog.ProductAggregate.Queries.ProductById;
using Domain.ShoppingVan.Exceptions;
using Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unit = Domain.ShoppingVan.AggregatesModel.ShoppingVanAggregate.Unit;

namespace Application.ShoppingVan.Commands.AddItemToVan
{
    public class AddItemToVanCommand : IRequest<int>
    {
        public string UnitId { get; set; }
        public string ProductId { get; set; }

        public class Handler : IRequestHandler<AddItemToVanCommand, int>
        {
            private readonly IShoppingVanRepository _shoppingVanRepository;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMediator _mediator;

            public Handler(
                IShoppingVanRepository shoppingVanRepository,
                ICurrentUserService currentUserService,
                IMediator mediator)
            {
                _shoppingVanRepository = shoppingVanRepository;
                _currentUserService = currentUserService;
                _mediator = mediator;
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

                var productToAddToVan = await _mediator.Send(new ProductByIdQuery { ProductId = request.ProductId });

                // selected product unit
                var selectedUnit = productToAddToVan.Units.FirstOrDefault(x => x.Id == request.UnitId);
                if (selectedUnit == null) throw new UnitNotFoundException(request.UnitId);

                // Adding product to van
                var unitsToAddToVan = new List<Unit>();

                foreach (var unit in productToAddToVan.Units)
                {
                    unitsToAddToVan.Add(new Unit(unit.Name, unit.Count, unit.ContentCount, unit.Price, unit.SellingPrice, unit.Weight, unit.IsAvailable, van.Id.ToString(), unit.Id));
                }

                van.AddItem(productToAddToVan.Id, productToAddToVan.Name, productToAddToVan.PhotoUrl, unitsToAddToVan, request.UnitId);

                _shoppingVanRepository.Update(van);

                await _shoppingVanRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return van.TotalItemsCount;
            }
        }
    }
}

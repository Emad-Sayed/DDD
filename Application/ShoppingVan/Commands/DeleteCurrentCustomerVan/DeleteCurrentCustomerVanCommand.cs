using Application.Common.Interfaces;
using Domain.ShoppingVan.Exceptions;
using Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ShoppingVan.Commands.DeleteCurrentCustomerVan
{
    public class DeleteCurrentCustomerVanCommand : IRequest<int>
    {

        public class Handler : IRequestHandler<DeleteCurrentCustomerVanCommand, int>
        {
            private readonly IShoppingVanRepository _shoppingVanRepository;
            private readonly ICurrentUserService _currentUserService;

            public Handler(
                IShoppingVanRepository shoppingVanRepository,
                ICurrentUserService currentUserService)
            {
                _shoppingVanRepository = shoppingVanRepository;
                _currentUserService = currentUserService;
            }

            public async Task<int> Handle(DeleteCurrentCustomerVanCommand request, CancellationToken cancellationToken)
            {
                // Get current logged in user shopping van
                var van = await _shoppingVanRepository.GetCustomerShoppingVan(_currentUserService.UserId);
                if (van == null) throw new EmptyShoppingVanException();

                _shoppingVanRepository.Delete(van);

                await _shoppingVanRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return van.TotalItemsCount;
            }
        }
    }
}

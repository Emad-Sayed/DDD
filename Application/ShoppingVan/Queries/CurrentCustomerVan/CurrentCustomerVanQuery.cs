using Application.Common.Interfaces;
using Application.ShoppingVan.ViewModels;
using AutoMapper;
using Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ShoppingVan.Queries.CurrentCustomerVan
{
    public class CurrentCustomerVanQuery : IRequest<ShoppingVanVM>
    {

        public class Handler : IRequestHandler<CurrentCustomerVanQuery, ShoppingVanVM>
        {
            private readonly IShoppingVanRepository _shoppingVanRepository;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;

            public Handler(IShoppingVanRepository shoppingVanRepository, ICurrentUserService currentUserService, IMapper mapper)
            {
                _shoppingVanRepository = shoppingVanRepository;
                _currentUserService = currentUserService;
                _mapper = mapper;
            }

            public async Task<ShoppingVanVM> Handle(CurrentCustomerVanQuery request, CancellationToken cancellationToken)
            {
                var vanFromRepo = await _shoppingVanRepository.FindByCustomerIdAsync(_currentUserService.UserId);

                var vanToReturn = _mapper.Map<ShoppingVanVM>(vanFromRepo);

                // TODO return empty object
                if (vanToReturn == null) vanToReturn = new ShoppingVanVM {
                    CustomerId = _currentUserService.UserId,
                    ShoppingVanItems = new List<ShoppingVanItemVM>()
                };

                return vanToReturn;
            }
        }
    }
}

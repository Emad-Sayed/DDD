using Application.Common.Interfaces;
using Application.ShoppingVanBoundedContext.ShoppingVanAggregate.ViewModels;
using AutoMapper;
using Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ShoppingVanBoundedContext.ShoppingVanAggregate.Queries.CurrentCustomerVan
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
                var vanFromRepo = await _shoppingVanRepository.FindByIdAsync(_currentUserService.UserId);

                var vanToReturn = _mapper.Map<ShoppingVanVM>(vanFromRepo);

                return vanToReturn;
            }
        }
    }
}

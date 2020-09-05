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

namespace Application.ShoppingVan.Queries.CurrentVanPricenfo
{
    public class CurrentVanPriceInfoQuery : IRequest<VanPriceInfoVM>
    {

        public class Handler : IRequestHandler<CurrentVanPriceInfoQuery, VanPriceInfoVM>
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

            public async Task<VanPriceInfoVM> Handle(CurrentVanPriceInfoQuery request, CancellationToken cancellationToken)
            {
                var vanFromRepo = await _shoppingVanRepository.FindByCustomerIdAsync(_currentUserService.UserId);
                if (vanFromRepo == null) return new VanPriceInfoVM { TaxValue = 14 };
                float taxValue = 14;
                var vanInfo = new VanPriceInfoVM
                {
                    TaxValue = taxValue,
                    TotalVanPriceBeforeTaxValue = vanFromRepo.TotalPrice,
                    TotalVanPriceAfterTaxValue = vanFromRepo.TotalPrice + (vanFromRepo.TotalPrice * 0.14f)
                };
                return vanInfo;
            }
        }
    }
}

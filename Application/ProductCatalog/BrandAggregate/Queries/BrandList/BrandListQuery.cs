using Application.Common.Models;
using Application.ProductCatalog.BrandAggregate.ViewModels;
using AutoMapper;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.BrandAggregate.Queries.BrandList
{
    public class BrandListQuery : IRequest<ListEntityVM<BrandVM>>
    {

        public class Handler : IRequestHandler<BrandListQuery, ListEntityVM<BrandVM>>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;

            public Handler(IBrandRepository brandRepository, IMapper mapper)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
            }

            public async Task<ListEntityVM<BrandVM>> Handle(BrandListQuery request, CancellationToken cancellationToken)
            {
                var (totalCount, brandsFromRepo) = await _brandRepository.GetAllBrands();

                var brandsToReturn = _mapper.Map<List<BrandVM>>(brandsFromRepo);

                return new ListEntityVM<BrandVM> { TotalCount = totalCount, Data = brandsToReturn };
            }
        }
    }
}

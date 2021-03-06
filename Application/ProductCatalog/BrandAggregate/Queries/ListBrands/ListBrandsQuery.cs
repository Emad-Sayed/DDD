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

namespace Application.ProductCatalog.BrandAggregate.Queries.ListBrands
{
    public class ListBrandsQuery : IRequest<ListEntityVM<BrandVM>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string KeyWord { get; set; }

        public class Handler : IRequestHandler<ListBrandsQuery, ListEntityVM<BrandVM>>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;

            public Handler(IBrandRepository brandRepository, IMapper mapper)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
            }

            public async Task<ListEntityVM<BrandVM>> Handle(ListBrandsQuery request, CancellationToken cancellationToken)
            {
                var (totalCount, brandsFromRepo) = await _brandRepository.GetBrands(request.PageNumber, request.PageSize, request.KeyWord);

                var brandsToReturn = _mapper.Map<List<BrandVM>>(brandsFromRepo);

                return new ListEntityVM<BrandVM> { TotalCount = totalCount, Data = brandsToReturn };
            }
        }
    }
}

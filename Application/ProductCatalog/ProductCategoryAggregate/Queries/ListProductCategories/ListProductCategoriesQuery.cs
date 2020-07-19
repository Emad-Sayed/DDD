using Application.Common.Models;
using Application.ProductCatalog.ProductCategoryAggregate.ViewModels;
using AutoMapper;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.ProductCategoryAggregate.Queries.ListProductCategories
{
    public class ListProductCategoriesQuery : IRequest<ListEntityVM<ProductCategoryVM>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string KeyWord { get; set; }

        public class Handler : IRequestHandler<ListProductCategoriesQuery, ListEntityVM<ProductCategoryVM>>
        {
            private readonly IProductCategoryRepository _productCategoryRepository;
            private readonly IMapper _mapper;

            public Handler(IProductCategoryRepository productCategoryRepository, IMapper mapper)
            {
                _productCategoryRepository = productCategoryRepository;
                _mapper = mapper;
            }

            public async Task<ListEntityVM<ProductCategoryVM>> Handle(ListProductCategoriesQuery request, CancellationToken cancellationToken)
            {
                var (totalCount, productCategorysFromRepo) = await _productCategoryRepository.GetProductCategorys(request.PageNumber, request.PageSize, request.KeyWord);

                var brandsToReturn = _mapper.Map<List<ProductCategoryVM>>(productCategorysFromRepo);

                return new ListEntityVM<ProductCategoryVM> { TotalCount = totalCount, Data = brandsToReturn };
            }
        }
    }
}

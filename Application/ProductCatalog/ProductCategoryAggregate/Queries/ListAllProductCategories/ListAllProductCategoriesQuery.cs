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

namespace Application.ProductCatalog.ProductCategoryAggregate.Queries.ProductCategoryList
{
    public class ListAllProductCategoriesQuery : IRequest<ListEntityVM<ProductCategoryVM>>
    {

        public class Handler : IRequestHandler<ListAllProductCategoriesQuery, ListEntityVM<ProductCategoryVM>>
        {
            private readonly IProductCategoryRepository _productCategoryRepository;
            private readonly IMapper _mapper;

            public Handler(IProductCategoryRepository productCategoryRepository, IMapper mapper)
            {
                _productCategoryRepository = productCategoryRepository;
                _mapper = mapper;
            }

            public async Task<ListEntityVM<ProductCategoryVM>> Handle(ListAllProductCategoriesQuery request, CancellationToken cancellationToken)
            {
                var (totalCount, productCategorysFromRepo) = await _productCategoryRepository.GetAllProductCategorys();

                var brandsToReturn = _mapper.Map<List<ProductCategoryVM>>(productCategorysFromRepo);

                return new ListEntityVM<ProductCategoryVM> { TotalCount = totalCount, Data = brandsToReturn };
            }
        }
    }
}

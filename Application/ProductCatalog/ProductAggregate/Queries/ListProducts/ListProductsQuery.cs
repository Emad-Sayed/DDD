using Application.Common.Models;
using Application.ProductCatalog.ProductAggregate.ViewModels;
using AutoMapper;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.ProductAggregate.Queries.ListProducts
{
    public class ListProductsQuery : IRequest<ListEntityVM<ProductVM>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string KeyWord { get; set; }
        public string BrandId { get; set; }
        public string ProductCategoryId { get; set; }
        public string DistributorId { get; set; }

        public class Handler : IRequestHandler<ListProductsQuery, ListEntityVM<ProductVM>>
        {
            private readonly IProductRepository _productsRepository;
            private readonly IMapper _mapper;

            public Handler(IProductRepository brandRepository, IMapper mapper)
            {
                _productsRepository = brandRepository;
                _mapper = mapper;
            }

            public async Task<ListEntityVM<ProductVM>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
            {
                var productsFromRepo = await _productsRepository.GetAllAsync(request.PageNumber, request.PageSize, request.KeyWord, request.BrandId, request.ProductCategoryId, request.DistributorId);

                var productsToReturn = _mapper.Map<List<ProductVM>>(productsFromRepo.Item2);

                return new ListEntityVM<ProductVM> { TotalCount = productsFromRepo.Item1, Data = productsToReturn };
            }
        }
    }
}

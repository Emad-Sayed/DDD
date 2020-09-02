using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.ProductAggregate.Commands.SyncProductsWithAlgolia
{

    public class SyncProductsWithAlgoliaCommand : IRequest
    {

        public class Handler : IRequestHandler<SyncProductsWithAlgoliaCommand>
        {
            private readonly IProductRepository _productRepository;
            private readonly IBrandRepository _brandRepository;
            private readonly IProductCategoryRepository _productCategoryRepository;
            private readonly ISearchEngine _searchEngine;

            public Handler(IProductRepository productRepository, ISearchEngine searchEngine, IProductCategoryRepository productCategoryRepository, IBrandRepository brandRepository)
            {
                _productRepository = productRepository;
                _searchEngine = searchEngine;
                _productCategoryRepository = productCategoryRepository;
                _brandRepository = brandRepository;
            }

            public async Task<MediatR.Unit> Handle(SyncProductsWithAlgoliaCommand request, CancellationToken cancellationToken)
            {
                var products = _searchEngine.ListProducts();

                _productRepository.DeleteAll();
                _brandRepository.DeleteAll();
                _productCategoryRepository.DeleteAll();

                _brandRepository.AddRange(products.Item1);
                _productCategoryRepository.AddRange(products.Item2);
                _productRepository.AddRange(products.Item3);

                await _productRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return MediatR.Unit.Value;
            }
        }
    }
}

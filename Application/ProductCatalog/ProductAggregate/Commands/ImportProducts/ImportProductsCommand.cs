﻿using Application.ProductCatalog.ProductAggregate.ViewModels;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.ProductAggregate.Commands.ImportProducts
{

    public class ImportProductsCommand : IRequest
    {
        public List<ProductToImportVM> Products { get; set; } = new List<ProductToImportVM>();

        public class Handler : IRequestHandler<ImportProductsCommand>
        {
            private readonly IProductRepository _productRepository;
            private readonly IBrandRepository _brandRepository;
            private readonly IProductCategoryRepository _productCategoryRepository;

            public Handler(IProductRepository productRepository, IBrandRepository brandRepository, IProductCategoryRepository productCategoryRepository)
            {
                _productRepository = productRepository;
                _brandRepository = brandRepository;
                _productCategoryRepository = productCategoryRepository;
            }

            public async Task<MediatR.Unit> Handle(ImportProductsCommand request, CancellationToken cancellationToken)
            {
                var products = new List<Product>();

                foreach (var productVM in request.Products)
                {
                    var brand = _brandRepository.AddBrandIfNotExist(productVM.Brand.Name);
                    var productCategory = _productCategoryRepository.AddProductCategoryIfNotExist(productVM.ProductCategory.Name);
                    
                    await _productRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                    var product = new Product(productVM.Name, string.Empty, string.Empty, true, brand.Id.ToString(), productCategory.Id.ToString());

                    foreach (var unitVM in productVM.Units)
                    {
                        product.AddUnitToProduct(unitVM.Name, unitVM.Count, 1, unitVM.Price, unitVM.SellingPrice, float.MinValue, true);
                    }

                    products.Add(product);
                }

                _productRepository.ImportProducts(products);

                await _productRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return MediatR.Unit.Value;
            }
        }
    }
}

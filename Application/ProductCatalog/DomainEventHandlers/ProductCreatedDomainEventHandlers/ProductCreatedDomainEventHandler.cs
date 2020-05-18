using Application.ProductCatalog.ProductAggregate.ViewModels;
using AutoMapper;
using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.DomainEventHandlers.ProductCreatedDomainEventHandlers
{
    public class ProductCreatedDomainEventHandler : INotificationHandler<ProductCreated>
    {
        private readonly IProductRepository _productRepository;
        private readonly ISearchEngine _searchEngine;
        private readonly IMapper _mapper;
        public ProductCreatedDomainEventHandler(ISearchEngine searchEngine, IMapper mapper, IProductRepository productRepository)
        {
            _searchEngine = searchEngine;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task Handle(ProductCreated notification, CancellationToken cancellationToken)
        {
            var productWithBrandAndCategory = await _productRepository.FindAsync(notification.Product.Id.ToString());

            var productToAddToAlgoia = _mapper.Map<AlgoliaProductVM>(productWithBrandAndCategory);

            await _searchEngine.AddEntity(productToAddToAlgoia, "products");
        }
    }
}

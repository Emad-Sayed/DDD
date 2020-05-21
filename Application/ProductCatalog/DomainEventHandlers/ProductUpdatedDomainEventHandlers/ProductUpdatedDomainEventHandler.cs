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

namespace Application.ProductCatalog.DomainEventHandlers.ProductUpdatedDomainEventHandlers
{
    public class ProductUpdatedDomainEventHandler : INotificationHandler<ProductUpdated>
    {
        private readonly IProductRepository _productRepository;
        private readonly ISearchEngine _searchEngine;
        private readonly IMapper _mapper;
        public ProductUpdatedDomainEventHandler(ISearchEngine searchEngine, IMapper mapper, IProductRepository productRepository)
        {
            _searchEngine = searchEngine;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task Handle(ProductUpdated notification, CancellationToken cancellationToken)
        {
            var productWithBrandAndCategory = await _productRepository.FindByIdAsync(notification.Product.Id.ToString());
            var productToAddToAlgoia =  _mapper.Map<AlgoliaProductVM>(productWithBrandAndCategory);
            await _searchEngine.UpdateEntity(productToAddToAlgoia, "products");
        }
    }
}

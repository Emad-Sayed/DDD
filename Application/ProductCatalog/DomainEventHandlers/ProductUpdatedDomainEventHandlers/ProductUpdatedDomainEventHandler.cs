using Application.Common.Interfaces;
using Application.ProductCatalog.ProductAggregate.ViewModels;
using AutoMapper;
using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.Events;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ProductUpdatedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public ProductUpdatedDomainEventHandler(ISearchEngine searchEngine, IMapper mapper, IProductRepository productRepository)
        {
            _searchEngine = searchEngine;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task Handle(ProductUpdated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(ProductUpdated), _currentUserService.UserId, _currentUserService.Name, notification);
            
            var productWithBrandAndCategory = await _productRepository.FindByIdAsync(notification.Product.Id.ToString());
            var productToAddToAlgoia =  _mapper.Map<AlgoliaProductVM>(productWithBrandAndCategory);
            await _searchEngine.UpdateEntity(productToAddToAlgoia, "products");
        }
    }
}

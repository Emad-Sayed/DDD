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

namespace Application.ProductCatalog.DomainEventHandlers.ProductCreatedDomainEventHandlers
{
    public class ProductCreatedDomainEventHandler : INotificationHandler<ProductCreated>
    {
        private readonly IProductRepository _productRepository;
        private readonly ISearchEngine _searchEngine;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductCreatedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public ProductCreatedDomainEventHandler(ISearchEngine searchEngine, IMapper mapper, IProductRepository productRepository, ILogger<ProductCreatedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _searchEngine = searchEngine;
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task Handle(ProductCreated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(ProductCreated), _currentUserService.UserId, _currentUserService.Name, notification);
           
            var productWithBrandAndCategory = await _productRepository.FindByIdAsync(notification.Product.Id.ToString());

            var productToAddToAlgoia = _mapper.Map<AlgoliaProductVM>(productWithBrandAndCategory);

            await _searchEngine.AddEntity(productToAddToAlgoia, "dev_product3");
        }
    }
}

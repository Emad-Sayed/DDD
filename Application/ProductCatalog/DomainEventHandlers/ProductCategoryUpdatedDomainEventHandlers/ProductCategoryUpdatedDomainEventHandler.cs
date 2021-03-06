using Application.Common.Interfaces;
using Application.ProductCatalog.ProductCategoryAggregate.ViewModels;
using AutoMapper;
using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using Domain.ProductCatalog.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.DomainEventHandlers.ProductCategoryUpdatedDomainEventHandlers
{
    public class ProductCategoryUpdatedDomainEventHandler : INotificationHandler<ProductCategoryUpdated>
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly ISearchEngine _searchEngine;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductCategoryUpdatedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public ProductCategoryUpdatedDomainEventHandler(ISearchEngine searchEngine, IMapper mapper, IProductCategoryRepository productCategoryRepository, ILogger<ProductCategoryUpdatedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _searchEngine = searchEngine;
            _productCategoryRepository = productCategoryRepository;
            _mapper = mapper;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task Handle(ProductCategoryUpdated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(ProductCategoryUpdated), _currentUserService.UserId, _currentUserService.Name, notification);

            var productCategory = await _productCategoryRepository.FindByIdAsync(notification.ProductCategory.Id.ToString());
            
            var productCategoryToAddToAlgoia = _mapper.Map<AlgoliaProductCategoryVM>(productCategory);
           
            await _searchEngine.UpdateEntity(productCategoryToAddToAlgoia, "productCategories");
        }
    }
}

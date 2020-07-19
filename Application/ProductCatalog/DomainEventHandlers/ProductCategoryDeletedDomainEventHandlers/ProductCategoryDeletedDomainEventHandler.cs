using Application.Common.Interfaces;
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

namespace Application.ProductCatalog.DomainEventHandlers.ProductCategoryDeletedDomainEventHandlers
{
    public class ProductCategoryDeletedDomainEventHandler : INotificationHandler<ProductCategoryDeleted>
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly ISearchEngine _searchEngine;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductCategoryDeletedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public ProductCategoryDeletedDomainEventHandler(ISearchEngine searchEngine, IMapper mapper, IProductCategoryRepository productCategoryRepository, ILogger<ProductCategoryDeletedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _searchEngine = searchEngine;
            _productCategoryRepository = productCategoryRepository;
            _mapper = mapper;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task Handle(ProductCategoryDeleted notification, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(ProductCategoryDeleted), _currentUserService.UserId, _currentUserService.Name, notification);
            await _searchEngine.DeleteEntity(notification.ProductCategory.Id.ToString(), "productCategories");
        }
    }
}

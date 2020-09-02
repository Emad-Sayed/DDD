using Application.Common.Interfaces;
using Application.ProductCatalog.ProductAggregate.ViewModels;
using AutoMapper;
using Domain.Common.Interfaces;
using Domain.ProductCatalog.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.DomainEventHandlers.ProductDeletedDomainEventHandlers
{
    public class ProductDeletedDomainEventHandler : INotificationHandler<ProductDeleted>
    {
        private readonly ISearchEngine _searchEngine;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductDeletedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public ProductDeletedDomainEventHandler(ISearchEngine searchEngine, IMapper mapper, ILogger<ProductDeletedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _searchEngine = searchEngine;
            _mapper = mapper;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task Handle(ProductDeleted notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(ProductDeleted), _currentUserService.UserId, _currentUserService.Name, notification);
            await _searchEngine.DeleteEntity(notification.Product.Id.ToString(), "dev_product3");
        }
    }
}

using Application.ProductCatalog.ProductAggregate.ViewModels;
using AutoMapper;
using Domain.Common.Interfaces;
using Domain.ProductCatalog.Events;
using MediatR;
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
        public ProductDeletedDomainEventHandler(ISearchEngine searchEngine, IMapper mapper)
        {
            _searchEngine = searchEngine;
            _mapper = mapper;
        }

        public async Task Handle(ProductDeleted notification, CancellationToken cancellationToken)
        {
            await _searchEngine.DeleteEntity(notification.Product.Id.ToString(), "products");
        }
    }
}

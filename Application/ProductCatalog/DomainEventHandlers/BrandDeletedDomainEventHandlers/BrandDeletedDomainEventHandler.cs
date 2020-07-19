using Application.Common.Interfaces;
using AutoMapper;
using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.DomainEventHandlers.BrandDeletedDomainEventHandlers
{
    public class BrandDeletedDomainEventHandler : INotificationHandler<BrandDeleted>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ISearchEngine _searchEngine;
        private readonly IMapper _mapper;
        private readonly ILogger<BrandDeletedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public BrandDeletedDomainEventHandler(ISearchEngine searchEngine, IMapper mapper, IBrandRepository brandRepository, ILogger<BrandDeletedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _searchEngine = searchEngine;
            _brandRepository = brandRepository;
            _mapper = mapper;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task Handle(BrandDeleted notification, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(BrandDeleted), _currentUserService.UserId, _currentUserService.Name, notification);

            await _searchEngine.DeleteEntity(notification.Brand.Id.ToString(), "brands");
        }
    }
}

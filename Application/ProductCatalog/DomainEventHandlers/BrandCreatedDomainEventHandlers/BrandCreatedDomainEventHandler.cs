using Application.Common.Interfaces;
using Application.ProductCatalog.BrandAggregate.ViewModels;
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

namespace Application.ProductCatalog.DomainEventHandlers.BrandCreatedDomainEventHandlers
{
    public class BrandCreatedDomainEventHandler : INotificationHandler<BrandCreated>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ISearchEngine _searchEngine;
        private readonly IMapper _mapper;
        private readonly ILogger<BrandCreatedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public BrandCreatedDomainEventHandler(ISearchEngine searchEngine, IMapper mapper, IBrandRepository brandRepository, ILogger<BrandCreatedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _searchEngine = searchEngine;
            _brandRepository = brandRepository;
            _mapper = mapper;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task Handle(BrandCreated notification, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(BrandCreated), _currentUserService.UserId, _currentUserService.Name, notification);
           
            var brand = await _brandRepository.FindByIdAsync(notification.Brand.Id.ToString());

            var brandToAddToAlgoia = _mapper.Map<AlgoliaBrandVM>(brand);

            await _searchEngine.AddEntity(brandToAddToAlgoia, "brands");
        }
    }
}

using Application.Common.Interfaces;
using Application.CustomerManagment.Commands.CreateCity;
using Domain.DistributorManagment.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.DomainEventHandlers.CityCreatedDomainEventHandlers
{
    public class CityCreatedDomainEventHandler : INotificationHandler<CityCreated>
    {
        private readonly ILogger<CityCreatedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public CityCreatedDomainEventHandler(ILogger<CityCreatedDomainEventHandler> logger, ICurrentUserService currentUserService, IMediator mediator)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public async Task Handle(CityCreated notification, CancellationToken cancellationToken)
        {
            // If city created successfully will create city in the customer cities table
            await _mediator.Send(new CreateCityCommand { CityId = notification.City.Id, Name = notification.City.Name }, cancellationToken);

            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(CityCreated), _currentUserService.UserId, _currentUserService.Name, notification);
        }
    }
}

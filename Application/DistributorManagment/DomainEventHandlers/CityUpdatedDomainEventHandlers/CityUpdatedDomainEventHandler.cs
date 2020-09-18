using Application.Common.Interfaces;
using Application.CustomerManagment.Commands.UpdateCity;
using Domain.DistributorManagment.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.DomainEventHandlers.CityUpdatedDomainEventHandlers
{
    public class CityUpdatedDomainEventHandler : INotificationHandler<CityUpdated>
    {
        private readonly ILogger<CityUpdatedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public CityUpdatedDomainEventHandler(ILogger<CityUpdatedDomainEventHandler> logger, ICurrentUserService currentUserService, IMediator mediator)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public async Task Handle(CityUpdated notification, CancellationToken cancellationToken)
        {
            // If city updated successfully will update city in the customer cities table
            await _mediator.Send(new UpdateCityCommand { CityId = notification.City.Id, Name = notification.City.Name }, cancellationToken);

            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(CityUpdated), _currentUserService.UserId, _currentUserService.Name, notification);
        }
    }
}

using Application.Common.Interfaces;
using Application.CustomerManagment.Commands.DeleteCity;
using Domain.DistributorManagment.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.DomainEventHandlers.CityDeletedDomainEventHandlers
{
    public class CityDeletedDomainEventHandler : INotificationHandler<CityDeleted>
    {
        private readonly ILogger<CityDeletedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public CityDeletedDomainEventHandler(ILogger<CityDeletedDomainEventHandler> logger, ICurrentUserService currentUserService, IMediator mediator)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public async Task Handle(CityDeleted notification, CancellationToken cancellationToken)
        {
            // If city deleted successfully will delete city in the customer cities table
            await _mediator.Send(new DeleteCityCommand { CityId = notification.City.Id }, cancellationToken);

            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(CityDeleted), _currentUserService.UserId, _currentUserService.Name, notification);
        }
    }
}

using Application.Common.Interfaces;
using Application.CustomerManagment.Commands.DeleteArea;
using Domain.DistributorManagment.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.DomainEventHandlers.AreaDeletedDomainEventHandlers
{
    public class AreaDeletedDomainEventHandler : INotificationHandler<AreaDeleted>
    {
        private readonly ILogger<AreaDeletedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public AreaDeletedDomainEventHandler(ILogger<AreaDeletedDomainEventHandler> logger, ICurrentUserService currentUserService, IMediator mediator)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public async Task Handle(AreaDeleted notification, CancellationToken cancellationToken)
        {
            // If area deleted successfully will delete area in the customer area table
            await _mediator.Send(new DeleteAreaCommand { CityId = notification.City.Id, AreaId = notification.Area.Id }, cancellationToken);

            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(AreaDeleted), _currentUserService.UserId, _currentUserService.Name, notification);

        }
    }
}

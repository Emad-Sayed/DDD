using Application.Common.Interfaces;
using Application.CustomerManagment.Commands.UpdateArea;
using Domain.DistributorManagment.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.DomainEventHandlers.AreaUpdatedDomainEventHandlers
{
    public class AreaUpdatedDomainEventHandler : INotificationHandler<AreaUpdated>
    {
        private readonly ILogger<AreaUpdatedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public AreaUpdatedDomainEventHandler(ILogger<AreaUpdatedDomainEventHandler> logger, ICurrentUserService currentUserService, IMediator mediator)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public async Task Handle(AreaUpdated notification, CancellationToken cancellationToken)
        {
            // If area updated successfully will update area in the customer area table
            await _mediator.Send(new UpdateAreaCommnad { CityId = notification.City.Id, AreaId = notification.Area.Id, Name = notification.Area.Name }, cancellationToken);

            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(AreaUpdated), _currentUserService.UserId, _currentUserService.Name, notification);
        }
    }
}

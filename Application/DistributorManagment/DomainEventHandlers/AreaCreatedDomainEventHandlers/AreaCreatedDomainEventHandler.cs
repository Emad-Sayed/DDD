using Application.Common.Interfaces;
using Application.CustomerManagment.Commands.CreateArea;
using Domain.DistributorManagment.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DistributorManagment.DomainEventHandlers.AreaCreatedDomainEventHandlers
{
    public class AreaCreatedDomainEventHandler : INotificationHandler<AreaCreated>
    {
        private readonly ILogger<AreaCreatedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public AreaCreatedDomainEventHandler(ILogger<AreaCreatedDomainEventHandler> logger, ICurrentUserService currentUserService, IMediator mediator)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }

        public async Task Handle(AreaCreated notification, CancellationToken cancellationToken)
        {
            // If area created successfully will creat area in the customer area table
             await _mediator.Send(new CreateAreaCommand { CityId = notification.Area.CityId, Name = notification.Area.Name }, cancellationToken);

            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(AreaCreated), _currentUserService.UserId, _currentUserService.Name, notification);
        }
    }
}

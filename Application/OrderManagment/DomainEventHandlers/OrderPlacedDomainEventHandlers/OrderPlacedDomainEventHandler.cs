using Application.Common.Interfaces;
using Application.ShoppingVan.Commands.DeleteCurrentCustomerVan;
using Application.ShoppingVan.Queries.CurrentCustomerVan;
using Domain.OrderManagment.Events;
using Domain.OrderManagment.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderManagment.DomainEventHandlers.OrderPlacedDomainEventHandlers
{
    public class OrderPlacedDomainEventHandler : INotificationHandler<OrderPlaced>
    {
        private readonly ILogger<OrderPlacedDomainEventHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public OrderPlacedDomainEventHandler(
            IMediator mediator, ILogger<OrderPlacedDomainEventHandler> logger, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task Handle(OrderPlaced notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brimo API EventHandelr: {Name} {@UserId} {@UserName} {@Request}", nameof(OrderPlaced), _currentUserService.UserId, _currentUserService.Name, notification);
            var customerVanFromQuery = await _mediator.Send(new CurrentCustomerVanQuery { }, cancellationToken);
            if (customerVanFromQuery != null) await _mediator.Send(new DeleteCurrentCustomerVanCommand { }, cancellationToken);
        }
    }
}

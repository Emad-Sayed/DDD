using Application.ShoppingVan.Commands.DeleteCurrentCustomerVan;
using Application.ShoppingVan.Queries.CurrentCustomerVan;
using Domain.OrderManagment.Events;
using Domain.OrderManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderManagment.DomainEventHandlers.OrderPlacedDomainEventHandlers
{
    public class OrderPlacedDomainEventHandler : INotificationHandler<OrderPlaced>
    {

        private readonly IMediator _mediator;

        public OrderPlacedDomainEventHandler(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(OrderPlaced notification, CancellationToken cancellationToken)
        {
            var customerVanFromQuery = await _mediator.Send(new CurrentCustomerVanQuery { }, cancellationToken);
            if (customerVanFromQuery != null) await _mediator.Send(new DeleteCurrentCustomerVanCommand { }, cancellationToken);
        }
    }
}

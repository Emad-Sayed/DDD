using Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ShoppingVanBoundedContext.Events
{
    public class ShoppingVanCreated : INotification
    {
        public Van ShoppingVan { get; private set; }
        public ShoppingVanCreated(Van shoppingVan)
        {
            ShoppingVan = shoppingVan;
        }
    }
}

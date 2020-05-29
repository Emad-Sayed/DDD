using Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ShoppingVanBoundedContext.Events
{
    public class ShoppingVanUpdated : INotification
    {
        public Van ShoppingVan { get; private set; }
        public ShoppingVanUpdated(Van shoppingVan)
        {
            ShoppingVan = shoppingVan;
        }
    }
}

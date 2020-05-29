using Domain.Base.Entity;
using Domain.Common.Interfaces;
using Domain.ShoppingVanBoundedContext.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate
{
    public class Van : EntityBase, IAggregateRoot
    {
        public ICollection<VanItem> ShoppingVanItems { get; private set; }
        public string CustomerId { get; private set; }
        public int TotalItems { get; private set; }

        private Van()
        {
        }

        public Van(string customerId)
        {
            CustomerId = customerId;

            // Add the ShoppingVanCreated to the domain events collection 
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            AddDomainEvent(new ShoppingVanCreated(this));
        }

        public void AddItem(string productId)
        {
            // Check if the product exist in the shopping van items or not if not will will add it with the required amount
            var vanItem = ShoppingVanItems.FirstOrDefault(x => x.ProductId == productId);
            if (vanItem == null)
            {
                ShoppingVanItems.Add(new VanItem(productId));
            }
            else
            {
                vanItem.ChangeAmount(1);
            }

            TotalItems += 1;

            AddDomainEvent(new ShoppingVanUpdated(this));
        }

        public void RemoveItem(string productId)
        {
            var vanItem = ShoppingVanItems.FirstOrDefault(x => x.ProductId == productId);
            if (vanItem != null)
            {
                TotalItems -= 1;
                vanItem.ChangeAmount(-1);

                // Check if the van item amount is less than or = to 0 then will remove this item from van
                if (vanItem.Amount <= 0) ShoppingVanItems.Remove(vanItem);

                AddDomainEvent(new ShoppingVanUpdated(this));
            }
        }

    }

}

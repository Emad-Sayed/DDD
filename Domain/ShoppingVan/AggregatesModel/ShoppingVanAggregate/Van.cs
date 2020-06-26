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
        public int TotalItemsCount { get; private set; }
        public float TotalPrice { get; private set; }

        private Van()
        {
        }

        public Van(string customerId)
        {
            CustomerId = customerId;
            TotalItemsCount = 0;
            ShoppingVanItems = new List<VanItem>();

            // Add the ShoppingVanCreated to the domain events collection 
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            AddDomainEvent(new ShoppingVanCreated(this));
        }

        public void AddItem(string productId, string productName, string unitId, string unitName, float unitPrice, string photoUrl, float sellingPrice)
        {
            // Check if the product exist in the shopping van items or not if not will will add it with the required amount
            var vanItem = ShoppingVanItems.FirstOrDefault(x => x.ProductId == productId);
            if (vanItem == null)
            {
                vanItem = new VanItem(Id.ToString(), productId, productName, unitId, unitName, unitPrice, photoUrl, sellingPrice);
                ShoppingVanItems.Add(vanItem);
            }
            else
            {
                vanItem.ChangeAmount(vanItem.Amount + 1);
            }

            TotalItemsCount += 1;
            TotalPrice += vanItem.SellingPrice + (vanItem.SellingPrice * 0.14f);

            AddDomainEvent(new ShoppingVanUpdated(this));
        }

        public void RemoveItem(string productId)
        {
            var vanItem = ShoppingVanItems.FirstOrDefault(x => x.ProductId == productId);
            if (vanItem != null)
            {
                TotalItemsCount -= 1;
                vanItem.ChangeAmount(vanItem.Amount - 1);
                TotalPrice -= vanItem.SellingPrice + (vanItem.SellingPrice * 0.14f);

                // Check if the van item amount is less than or = to 0 then will remove this item from van
                if (vanItem.Amount <= 0) ShoppingVanItems.Remove(vanItem);

                AddDomainEvent(new ShoppingVanUpdated(this));
            }
        }

    }

}

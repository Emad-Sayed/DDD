using Domain.Base.Entity;
using Domain.Common.Interfaces;
using Domain.ShoppingVan.AggregatesModel.ShoppingVanAggregate;
using Domain.ShoppingVan.Exceptions;
using Domain.ShoppingVanBoundedContext.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate
{
    public class Van : AuditableEntity, IAggregateRoot
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

        public void AddItem(string productId, string productName, string photoUrl, List<Unit> units, string unitId)
        {
            // Check if the product exist in the shopping van items or not if not will will add it with the required amount
            var vanItem = ShoppingVanItems.FirstOrDefault(x => x.ProductId == productId);
            if (vanItem == null)
            {
                vanItem = new VanItem(Id.ToString(), productId, productName, photoUrl, units);
                ShoppingVanItems.Add(vanItem);
            }

            var unit = vanItem.IncreaseUnit(unitId);

            TotalItemsCount += 1;
            TotalPrice += unit.SellingPrice;

            AddDomainEvent(new ShoppingVanUpdated(this));
        }

        public void RemoveItem(string productId, string unitId)
        {
            var vanItem = ShoppingVanItems.FirstOrDefault(x => x.ProductId == productId);
            if (vanItem == null) throw new ShoppingVanItemNotFound(productId, unitId);

            TotalItemsCount -= 1;
            var unit = vanItem.DecreaseUnit(unitId);
            TotalPrice -= unit.SellingPrice;

            AddDomainEvent(new ShoppingVanUpdated(this));
        }

    }

}

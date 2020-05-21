using Domain.Base.Entity;
using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate
{
    public class ShoppingVan : EntityBase, IAggregateRoot
    {
        public ICollection<ShoppingVanItem> ShoppingVanItems { get; private set; }
        public string CustomerId { get; set; }

        private ShoppingVan()
        {
        }

        public ShoppingVan(string customerId)
        {

        }

        public void AddItem(string productId, int amount = 1)
        {
            var vanItem = ShoppingVanItems.FirstOrDefault(x => x.ProductId == productId);
            if (vanItem == null)
            {
                ShoppingVanItems.Add(new ShoppingVanItem(productId, amount));
            }
            else
            {
                vanItem.changeAmount(amount);
            }
        }

        public void ChangeAmount(string productId, int amount = 1)
        {
            var vanItem = ShoppingVanItems.FirstOrDefault(x => x.ProductId == productId);
            if (vanItem != null)
            {
                ShoppingVanItems.Remove(vanItem);
            }
        }
    }

}

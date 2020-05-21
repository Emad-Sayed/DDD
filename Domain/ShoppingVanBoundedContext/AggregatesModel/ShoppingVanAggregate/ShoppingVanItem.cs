using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate
{
    public class ShoppingVanItem : EntityBase
    {
        public string ProductId { get; private set; }
        public int Amount { get; private set; }

        private ShoppingVanItem() { }
        public ShoppingVanItem(string productId, int amount)
        {
            ProductId = productId;
            Amount = amount;
        }

        public void changeAmount(int amount)
        {
            Amount = amount;
        }
    }
}

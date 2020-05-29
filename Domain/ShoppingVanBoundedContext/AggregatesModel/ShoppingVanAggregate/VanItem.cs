using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate
{
    public class VanItem : EntityBase
    {
        public string ProductId { get; private set; }
        public int Amount { get; private set; }

        private VanItem() { }
        public VanItem(string productId)
        {
            ProductId = productId;
            Amount = 1;
        }

        public void ChangeAmount(int amount)
        {
            Amount = amount;
        }
    }
}

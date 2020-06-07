using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate
{
    public class VanItem : EntityBase
    {
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string UnitId { get; private set; }
        public string UnitName { get; private set; }
        public float UnitPrice { get; private set; }
        public string PhotoUrl { get; private set; }
        public float SellingPrice { get; private set; }
        public int Amount { get; private set; }

        public string VanId { get; private set; }
        public Van Van { get; private set; }

        private VanItem() { }
        public VanItem(string vanId, string productId, string productName, string unitId, string unitName, float unitPrice, string photoUrl, float sellingPrice)
        {
            ProductId = productId;
            ProductName = productName;
            VanId = vanId;
            UnitId = unitId;
            UnitName = unitName;
            UnitPrice = unitPrice;
            PhotoUrl = photoUrl;
            SellingPrice = sellingPrice;

            Amount = 1;
        }

        public void ChangeAmount(int amount)
        {
            Amount = amount;
        }
    }
}

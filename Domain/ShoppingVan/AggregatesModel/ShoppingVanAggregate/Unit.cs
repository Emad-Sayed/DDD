using Domain.Base.Entity;
using Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ShoppingVan.AggregatesModel.ShoppingVanAggregate
{
    public class Unit : AuditableEntity
    {
        public string Name { get; private set; }

        // How many units from this unit
        public int Count { get; private set; }
        public int CustomerCount { get; private set; }

        // How many item in this unit
        public int ContentCount { get; private set; }

        // The price of 1 unit
        public float Price { get; private set; }

        // The Selling Price for 1 unit
        public float SellingPrice { get; set; }

        // The Weight of 1 unit
        public float Weight { get; private set; }

        // Is this unit enabled and can be used
        public bool IsAvailable { get; private set; }
        public string UnitId { get; set; }

        public string VanItemId { get; private set; }
        public VanItem VanItem { get; private set; }
        public bool IsDeleted { get; private set; }

        public Unit() { }

        public Unit(string name, int count, int contentCount, float price, float sellingPrice, float weight, bool isAvailable, string vanItemId, string unitId)
        {
            Name = name;
            Count = count;
            ContentCount = contentCount;
            Price = price;
            SellingPrice = sellingPrice;
            Weight = weight;
            IsAvailable = isAvailable;
            VanItemId = vanItemId;
            UnitId = unitId;
        }

        public void Update(string name, int count, int contentCount, float price, float sellingPrice, float weight, bool isAvailable)
        {
            Name = name;
            Count = count;
            ContentCount = contentCount;
            Price = price;
            SellingPrice = sellingPrice;
            Weight = weight;
            IsAvailable = isAvailable;
        }

        public void DeleteUnit()
        {
            IsDeleted = true;
        }

        public void DecreaseUnit()
        {
            CustomerCount = --CustomerCount;
        }

        public void IncreaseUnit()
        {
            CustomerCount = ++CustomerCount;
        }
    }
}

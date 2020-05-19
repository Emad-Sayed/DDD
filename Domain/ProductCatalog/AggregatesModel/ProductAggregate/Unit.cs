using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.AggregatesModel.ProductAggregate
{
    public class Unit : EntityBase
    {
        public string Name { get; private set; }

        // How many units from this unit
        public int Count { get; private set; }

        // How many item in this unit
        public int ContentCount { get; private set; }

        // The price of 1 unit
        public float Price { get; private set; }

        // The Selling Price for 1 unit
        public float SellingPrice { get; set; }

        // The Weight of 1 unit
        public float Weight { get; private set; }

        // Is this unit enabled and can be used
        public bool IsAvilable { get; private set; }

        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }

        private Unit() { }

        public Unit(string name, int count, int contentCount, float price, float sellingPrice, float weight, bool isAvilable, Guid productId)
        {
            Name = name;
            Count = count;
            ContentCount = contentCount;
            Price = price;
            SellingPrice = sellingPrice;
            Weight = weight;
            IsAvilable = isAvilable;
            ProductId = productId;
        }

        public void Update(string name, int count, int contentCount, float price, float sellingPrice, float weight, bool isAvilable)
        {
            Name = name;
            Count = count;
            ContentCount = contentCount;
            Price = price;
            SellingPrice = sellingPrice;
            Weight = weight;
            IsAvilable = isAvilable;
        }
    }
}

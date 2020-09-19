using Domain.Base.Entity;
using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Domain.OffersManagment.AggregatesModel
{
    public class Product : AuditableEntity, IAggregateRoot
    {
        public string ProductId { get; private set; }
        public string Name { get; private set; }
        public string Barcode { get; private set; }
        public string PhotoUrl { get; private set; }
        public bool AvailableToSell { get; set; }

        public string Brand { get; private set; }

        public string ProductCategory { get; private set; }

        public ICollection<Unit> Units { get; private set; }
        public bool IsDeleted { get; private set; }

        public Product()
        {
            Units = new List<Unit>();
        }

        public Product(string productId, string name, string barcode, string photoUrl, bool availableToSell, string brand, string productCategory)
        {
            ProductId = productId;
            Name = name;
            Barcode = barcode;
            PhotoUrl = photoUrl;
            AvailableToSell = availableToSell;
            Brand = brand;
            ProductCategory = productCategory;

            Units = new List<Unit>();
        }

        public void AddUnit(string unitId, string name, int count, int contentCount, float price, float sellingPrice, float weight, bool isAvailable)
        {
            var unit = new Unit(unitId, name, count, contentCount, price, sellingPrice, weight, isAvailable, Id);
            Units.Add(unit);
        }


    }
}

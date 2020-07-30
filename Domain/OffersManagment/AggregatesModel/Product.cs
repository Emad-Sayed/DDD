using Domain.Base.Entity;
using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
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

        public Product(string productId, string name, string barcode, string photoUrl, bool availableToSell, string brand, string productCategory, Guid id = default)
        {
            ProductId = productId;
            Name = name;
            Barcode = barcode;
            PhotoUrl = photoUrl;
            AvailableToSell = availableToSell;
            Brand = brand;
            ProductCategory = productCategory;

            Id = id == default ? Guid.NewGuid() : id;
        }


    }
}

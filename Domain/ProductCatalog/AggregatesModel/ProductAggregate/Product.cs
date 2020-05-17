using Domain.Base.Entity;
using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using Domain.ProductCatalog.Events;
using System;

namespace Domain.ProductCatalog.AggregatesModel.ProductAggregate
{
    public class Product : EntityBase, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Barcode { get; private set; }
        public string PhotoUrl { get; private set; }
        public bool AvailableToSell { get; set; }

        public Guid BrandId { get; private set; }
        public Brand Brand { get; private set; }

        public Guid ProductCategoryId { get; private set; }
        public ProductCategory ProductCategory { get; private set; }

        private Product() { }

        public Product(string name, string barcode, string photoUrl,bool availableToSell, string brandId, string productCategoryId, Guid id = default)
        {
            Name = name;
            Barcode = barcode;
            PhotoUrl = photoUrl;
            AvailableToSell = availableToSell;
            BrandId = new Guid(brandId);
            ProductCategoryId = new Guid(productCategoryId);

            Id = id == default ? Guid.NewGuid() : id;

            // TODO Failer in Db If it will 

            // Add the ProductCreated to the domain events collection 
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            AddDomainEvent(new ProductCreated(this));
        }
    }
}

using Domain.Base.Entity;
using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using Domain.ProductCatalog.Events;
using System;
using System.Collections;
using System.Collections.Generic;

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

        public ICollection<Unit> Units { get; private set; }

        private Product()
        {
            Units = new List<Unit>();
        }

        public Product(string name, string barcode, string photoUrl, bool availableToSell, string brandId, string productCategoryId, Guid id = default)
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

        public void UpdateProduct(string name, string barcode, string photoUrl, bool availableToSell)
        {
            Name = name;
            Barcode = barcode;
            PhotoUrl = photoUrl;
            AvailableToSell = availableToSell;

            // rais product updated event
            AddDomainEvent(new ProductUpdated(this));
        }

        public void AddUnitToProduct(string name, int count, int contentCount, float price, float weight, bool isAvilable)
        {
            var newProductUnit = new Unit(name, count, contentCount, price, weight, isAvilable, this.Id);
            Units.Add(newProductUnit);

            // rais product updated event
            AddDomainEvent(new ProductUpdated(this));
        }
    }
}

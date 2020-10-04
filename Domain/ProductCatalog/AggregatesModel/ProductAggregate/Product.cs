using Domain.Base.Entity;
using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using Domain.ProductCatalog.Events;
using Domain.ProductCatalog.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.ProductCatalog.AggregatesModel.ProductAggregate
{
    public class Product : AuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Barcode { get; private set; }
        public string PhotoUrl { get; private set; }
        public bool AvailableToSell { get; set; }
        public string DistributorId { get; private set; }

        public Guid BrandId { get; private set; }
        public Brand Brand { get; private set; }

        public Guid ProductCategoryId { get; private set; }
        public ProductCategory ProductCategory { get; private set; }

        public ICollection<Unit> Units { get; private set; }
        public bool IsDeleted { get; private set; }

        public Product()
        {
            Units = new List<Unit>();
        }

        public Product(string distributorId, string name, string barcode, string photoUrl, bool availableToSell, string brandId, string productCategoryId, Guid id = default)
        {
            DistributorId = distributorId;
            Name = name;
            Barcode = barcode;
            PhotoUrl = photoUrl;
            AvailableToSell = availableToSell;
            BrandId = new Guid(brandId);
            ProductCategoryId = new Guid(productCategoryId);

            Id = id == default ? Guid.NewGuid() : id;
            Units = new List<Unit>();
            // Add the ProductCreated to the domain events collection
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            AddDomainEvent(new ProductCreated(this));
        }

        // update product
        public void UpdateProduct(string distributorId, string name, string barcode, string photoUrl, bool availableToSell, string brandId, string productCategoryId)
        {
            DistributorId = distributorId;
            Name = name;
            Barcode = barcode;
            PhotoUrl = photoUrl;
            AvailableToSell = availableToSell;
            BrandId = new Guid(brandId);
            ProductCategoryId = new Guid(productCategoryId);

            // rais product updated event
            AddDomainEvent(new ProductUpdated(this));
        }

        // delete product
        public void DeleteProduct()
        {
            IsDeleted = true;

            foreach (var unit in Units)
            {
                unit.DeleteUnit();
            }
            // rais product deleted event
            AddDomainEvent(new ProductDeleted(this));
        }

        // add unit to product
        public Unit AddUnitToProduct(string name, int count, int contentCount, float price, float sellingPrice, float weight, bool isAvailable)
        {
            var newProductUnit = new Unit(name, count, contentCount, price, sellingPrice, weight, isAvailable, Id);
            Units.Add(newProductUnit);

            // rais product updated event
            AddDomainEvent(new ProductUpdated(this));

            return newProductUnit;
        }

        // update unit product
        public void UpdateProductUnit(string unitId, string name, int count, int contentCount, float price, float sellingPrice, float weight, bool isAvailable)
        {
            var unitToUpdate = Units.FirstOrDefault(x => x.Id.ToString() == unitId);
            if (unitToUpdate == null) throw new UnitNotFoundException(unitId);

            unitToUpdate.Update(name, count, contentCount, price, sellingPrice, weight, isAvailable);

            // rais product updated event
            AddDomainEvent(new ProductUpdated(this));
        }

        // Remove unit from product
        public void DeleteProductUnit(string unitId)
        {
            var unitToDelete = Units.FirstOrDefault(x => x.Id.ToString() == unitId);
            if (unitToDelete == null) throw new UnitNotFoundException(unitId);

            unitToDelete.DeleteUnit();

            // rais product updated event
            AddDomainEvent(new ProductUpdated(this));
        }
    }
}
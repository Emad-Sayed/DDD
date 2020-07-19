using Domain.Base.Entity;
using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.Events;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate
{
    public class ProductCategory : AuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string PhotoUrl { get; private set; }

        public ICollection<Product> Products { get; private set; }


        private ProductCategory() { }

        public ProductCategory(string name,string photoUrl = null, Guid id = default)
        {
            Name = name;
            PhotoUrl = photoUrl;
            Id = id == default ? Guid.NewGuid() : id;

            // Add the ProductCategoryCreated to the domain events collection 
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            AddDomainEvent(new ProductCategoryCreated(this));
        }

        // update productCategory
        public void Update(string name, string photoUrl)
        {
            Name = name;
            PhotoUrl = photoUrl;

            // rais productCategory updated event
            AddDomainEvent(new ProductCategoryUpdated(this));
        }


        // delete productCategory
        public void Delete()
        {
            AddDomainEvent(new ProductCategoryDeleted(this));
        }

    }
}

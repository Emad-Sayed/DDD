using Domain.Base.Entity;
using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.Events;
using System;
using System.Collections.Generic;

namespace Domain.ProductCatalog.AggregatesModel.BrandAggregate
{
    public class Brand : AuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string PhotoUrl { get; private set; }

        public ICollection<Product> Products { get; private set; }

        public Brand() { }

        public Brand(string name,string photoUrl = null, Guid id = default)
        {
            Name = name;
            PhotoUrl = photoUrl;
            Id = id == default ? Guid.NewGuid() : id;

            // Add the BrandCreated to the domain events collection 
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            AddDomainEvent(new BrandCreated(this));
        }


        // update brand
        public void Update(string name, string photoUrl)
        {
            Name = name;
            PhotoUrl = photoUrl;

            // rais brand updated event
            AddDomainEvent(new BrandUpdated(this));
        }


        // delete brand
        public void Delete()
        {
            AddDomainEvent(new BrandDeleted(this));
        }

    }
}

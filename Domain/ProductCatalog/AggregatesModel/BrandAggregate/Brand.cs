using Domain.Base.Entity;
using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;

namespace Domain.ProductCatalog.AggregatesModel.BrandAggregate
{
    public class Brand : AuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; }

        public ICollection<Product> Products { get; private set; }

        private Brand() { }

        public Brand(string name, Guid id = default)
        {
            Name = name;
            Id = id == default ? Guid.NewGuid() : id;
        }
    }
}

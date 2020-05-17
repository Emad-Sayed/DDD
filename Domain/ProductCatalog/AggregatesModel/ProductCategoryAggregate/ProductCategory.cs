using Domain.Base.Entity;
using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate
{
    public class ProductCategory : EntityBase, IAggregateRoot
    {
        public string Name { get; private set; }

        public ICollection<Product> Products { get; private set; }


        private ProductCategory() { }

        public ProductCategory(string name, Guid id = default)
        {
            Name = name;
            Id = id == default ? Guid.NewGuid() : id;
        }
    }
}

using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Events
{
    public class ProductDeleted : INotification
    {
        public Product Product { get; private set; }
        public ProductDeleted(Product product)
        {
            Product = product;
        }
    }
}

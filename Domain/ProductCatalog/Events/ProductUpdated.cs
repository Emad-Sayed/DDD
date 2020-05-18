using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Events
{
    public class ProductUpdated : INotification
    {
        public Product Product { get; private set; }
        public ProductUpdated(Product product)
        {
            Product = product;
        }
    }
}

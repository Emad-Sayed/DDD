using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Events
{
    public class ProductCategoryUpdated : INotification
    {
        public ProductCategory ProductCategory { get; private set; }
        public ProductCategoryUpdated(ProductCategory productCategory)
        {
            ProductCategory = productCategory;
        }
    }
}

using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ProductCatalog.Events
{
    public class ProductCategoryDeleted : INotification
    {
        public ProductCategory ProductCategory { get; private set; }
        public ProductCategoryDeleted(ProductCategory productCategory)
        {
            ProductCategory = productCategory;
        }
    }
}

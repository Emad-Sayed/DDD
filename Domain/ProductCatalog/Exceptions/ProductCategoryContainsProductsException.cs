using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.ProductCatalog.Exceptions
{
    public class ProductCategoryContainsProductsException : BusinessException
    {
        public ProductCategoryContainsProductsException(string id)
           : base(HttpStatusCode.BadRequest, $"ProductCategory with id {id} was contains products.", "product_category_contains_products")
        {
        }
    }
}

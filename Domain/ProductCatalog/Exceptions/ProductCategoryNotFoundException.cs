using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.ProductCatalog.Exceptions
{
    public class ProductCategoryNotFoundException : BusinessException
    {
        public ProductCategoryNotFoundException(string id)
           : base(HttpStatusCode.NotFound, $"ProductCategory with id {id} was not found.", "product_category_notfound")
        {
        }
    }
}

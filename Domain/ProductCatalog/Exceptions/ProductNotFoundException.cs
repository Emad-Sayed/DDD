using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.ProductCatalog.Exceptions
{
    public class ProductNotFoundException : BusinessException
    {
        public ProductNotFoundException(string id)
           : base(HttpStatusCode.NotFound, $"Product with id {id} was not found.", "product_notfound")
        {
        }
    }
}

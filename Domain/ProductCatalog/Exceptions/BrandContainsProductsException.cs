using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.ProductCatalog.Exceptions
{
    public class BrandContainsProductsException : BusinessException
    {
        public BrandContainsProductsException(string id)
           : base(HttpStatusCode.BadRequest, $"Brand with id {id} was contains products.", "brand_contains_products")
        {
        }
    }
}

using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.ProductCatalog.Exceptions
{
    public class BrandNotFoundException : BusinessException
    {
        public BrandNotFoundException(string id)
           : base(HttpStatusCode.NotFound, $"Brand with id {id} was not found.", "brand_notfound")
        {
        }
    }
}

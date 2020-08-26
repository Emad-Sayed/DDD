using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.ShoppingVan.Exceptions
{
    public class ShoppingVanItemNotFound : BusinessException
    {
        public ShoppingVanItemNotFound(string productId, string unitId)
           : base(HttpStatusCode.NotFound, $"Product with id {productId} and unitId {unitId} was not found in the van.", "product_notfound_in_van")
        {
        }
    }
}

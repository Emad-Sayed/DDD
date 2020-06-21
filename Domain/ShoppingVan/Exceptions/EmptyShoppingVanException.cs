using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.ShoppingVan.Exceptions
{
    public class EmptyShoppingVanException : BusinessException
    {
        public EmptyShoppingVanException()
           : base(HttpStatusCode.BadRequest, $"Shopping van is empty", "empty_shopping_van")
        {
        }
    }
}

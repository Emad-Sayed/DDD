using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.ShoppingVan.Exceptions
{
    public class InValidCusotmerUnitCountException : BusinessException
    {
        public InValidCusotmerUnitCountException()
           : base(HttpStatusCode.BadRequest, $"Cusotmer Unit count must be grater than zero", "invalid_customer_unit_count")
        {
        }
    }
}

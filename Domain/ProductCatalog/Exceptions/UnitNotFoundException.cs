using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.ProductCatalog.Exceptions
{
    public class UnitNotFoundException : BusinessException
    {
        public UnitNotFoundException(string id)
           : base(HttpStatusCode.NotFound, $"Unit with id {id} was not found.", "unit_notfound")
        {
        }
    }
}

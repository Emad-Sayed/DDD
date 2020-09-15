using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.CustomerManagment.Exceptions
{
    public class AreaNotFoundException : BusinessException
    {
        public AreaNotFoundException(string id)
           : base(HttpStatusCode.NotFound, $"Area with id {id} was not found.", "area_notfound")
        {
        }
    }
}

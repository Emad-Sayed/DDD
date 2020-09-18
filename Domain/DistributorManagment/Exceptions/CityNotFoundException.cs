using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.DistributorManagment.Exceptions
{
    public class CityNotFoundException : BusinessException
    {
        public CityNotFoundException(string id)
           : base(HttpStatusCode.NotFound, $"City with id {id} was not found.", "city_notfound")
        {
        }
    }
}

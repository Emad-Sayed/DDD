using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.DistributorManagment.Exceptions
{
    public class DistributorNotFoundException : BusinessException
    {
        public DistributorNotFoundException(string id)
           : base(HttpStatusCode.NotFound, $"Distributor with id {id} was not found.", "distributor_notfound")
        {
        }
    }
}

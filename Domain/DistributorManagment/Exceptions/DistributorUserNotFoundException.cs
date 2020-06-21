using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.DistributorManagment.Exceptions
{
    public class DistributorUserNotFoundException : BusinessException
    {
        public DistributorUserNotFoundException(string id)
           : base(HttpStatusCode.NotFound, $"Distributor User with id {id} was not found.", "distributor_user_notfound")
        {
        }
    }
}

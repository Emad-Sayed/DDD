using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.DistributorManagment.Exceptions
{
    public class DistributorContainsUsersException : BusinessException
    {
        public DistributorContainsUsersException(string id)
           : base(HttpStatusCode.BadRequest, $"Distributor with id {id} was contains users", "distributor_contains_users")
        {
        }
    }
}

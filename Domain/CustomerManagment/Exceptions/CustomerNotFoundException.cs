using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.CustomerManagment.Exceptions
{
    public class CustomerNotFoundException : BusinessException
    {
        public CustomerNotFoundException(string id)
           : base(HttpStatusCode.NotFound, $"Customer with id {id} was not found.", "customer_notfound")
        {
        }
    }
}

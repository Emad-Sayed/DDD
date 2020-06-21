using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.OrderManagment.Exceptions
{
    public class OrderNotFoundException : BusinessException
    {
        public OrderNotFoundException(string id)
           : base(HttpStatusCode.NotFound, $"Order with id {id} was not found.", "order_notfound")
        {
        }
    }
}

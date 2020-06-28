using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.OrderManagment.Exceptions
{
    public class OrderNotShippedException : BusinessException
    {
        public OrderNotShippedException(string id)
           : base(HttpStatusCode.BadRequest, $"Order with id {id} is not shipped order.", "order_not_shipped")
        {
        }
    }
}

using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.OrderManagment.Exceptions
{
    public class OrderNotPlacedException : BusinessException
    {
        public OrderNotPlacedException(string id)
           : base(HttpStatusCode.BadRequest, $"Order with id {id} is not placed order.", "order_not_placed")
        {
        }
    }
}

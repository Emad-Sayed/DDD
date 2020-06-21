using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.OrderManagment.Exceptions
{
    public class OrderItemNotFoundException : BusinessException
    {
        public OrderItemNotFoundException(string id)
           : base(HttpStatusCode.NotFound, $"Order item with id {id} was not found.", "order_item_notfound")
        {
        }
    }
}

using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.OrderManagment.Exceptions
{
    public class OrderNotConfirmedException : BusinessException
    {
        public OrderNotConfirmedException(string id)
           : base(HttpStatusCode.BadRequest, $"Order with id {id} is not confirmed order.", "order_not_confirmed")
        {
        }
    }
}

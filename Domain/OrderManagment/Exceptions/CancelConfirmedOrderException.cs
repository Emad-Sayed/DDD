using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.OrderManagment.Exceptions
{
    public class CancelConfirmedOrderException : BusinessException
    {
        public CancelConfirmedOrderException(string id)
           : base(HttpStatusCode.BadRequest, $"Can cancel confirmed order with id {id}", "order_cancel_after_confirmed")
        {
        }
    }
}

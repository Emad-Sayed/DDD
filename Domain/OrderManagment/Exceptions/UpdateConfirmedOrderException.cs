using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.OrderManagment.Exceptions
{
    public class UpdateConfirmedOrderException : BusinessException
    {
        public UpdateConfirmedOrderException(string id)
           : base(HttpStatusCode.BadRequest, $"Order with id {id} is not placed order.", "can_not_update_confirmed_order")
        {
        }
    }
}

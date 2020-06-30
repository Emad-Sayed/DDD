using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Domain.Common.Exceptions;

namespace Domain.OrderManagment.Exceptions
{
    public class ProductExitInOrderException : BusinessException
    {
        public ProductExitInOrderException(string id)
            : base(HttpStatusCode.BadRequest, $"Product with id {id} is already in the order.", "product_already_exist_in_order")
        {
        }
    }
}

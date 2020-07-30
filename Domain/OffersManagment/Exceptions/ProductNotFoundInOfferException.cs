using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.OffersManagment.Exceptions
{
    public class ProductNotFoundInOfferException : BusinessException
    {
        public ProductNotFoundInOfferException(string id)
           : base(HttpStatusCode.NotFound, $"Product with id {id} was not found in offer.", "product_notfound")
        {
        }
    }
}

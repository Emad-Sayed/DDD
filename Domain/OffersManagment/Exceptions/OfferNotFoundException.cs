using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.OffersManagment.Exceptions
{
    public class OfferNotFoundException : BusinessException
    {
        public OfferNotFoundException(string id)
           : base(HttpStatusCode.NotFound, $"Offer with id {id} was not found.", "offer_notfound")
        {
        }
    }
}

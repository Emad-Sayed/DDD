using Domain.OffersManagment.AggregatesModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.OffersManagment.Events
{
    public class OfferDeleted : INotification
    {
        public Offer Offer { get; private set; }
        public OfferDeleted(Offer offer)
        {
            Offer = offer;
        }
    }
}

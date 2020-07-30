using Domain.OffersManagment.AggregatesModel;
using MediatR;

namespace Domain.OffersManagment.Events
{
    public class OfferUpdated : INotification
    {
        public Offer Offer { get; private set; }

        public OfferUpdated(Offer offer)
        {
            Offer = offer;
        }
    }
}
using Domain.OffersManagment.AggregatesModel;
using MediatR;

namespace Domain.OffersManagment.Events
{
    public class OfferCreated : INotification
    {
        public Offer Offer { get; private set; }

        public OfferCreated(Offer offer)
        {
            Offer = offer;
        }
    }
}
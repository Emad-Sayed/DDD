using Domain.OffersManagment.AggregatesModel;
using Domain.OffersManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OffersManagment.Commands.DeleteOffer
{
    public class DeleteOfferCommand : IRequest
    {

        public string OfferId { get; set; }


        public class Handler : IRequestHandler<DeleteOfferCommand>
        {
            private readonly IOfferRepository _offerRepository;

            public Handler(IOfferRepository offerRepository)
            {
                _offerRepository = offerRepository;
            }

            public async Task<MediatR.Unit> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
            {
                // get offer by id
                var offerFromRepo = await _offerRepository.FindByIdAsync(request.OfferId);
                if (offerFromRepo == null) throw new OfferNotFoundException(request.OfferId);


                // we call delete offer to rase delete offer event to sync with algolia
                offerFromRepo.DeleteOffer();

                // update offer with the new unit deleted
                _offerRepository.Update(offerFromRepo);

                // save changes in the database and rase OfferUpdated event
                await _offerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return MediatR.Unit.Value;
            }
        }
    }
}

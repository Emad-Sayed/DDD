using Domain.OffersManagment.AggregatesModel;
using Domain.OffersManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OffersManagment.Commands.UpdateOffer
{
    public class UpdateOfferCommand : IRequest<string>
    {
        public string OfferId { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public class Handler : IRequestHandler<UpdateOfferCommand, string>
        {
            private readonly IOfferRepository _offerRepository;

            public Handler(IOfferRepository offerRepository)
            {
                _offerRepository = offerRepository;
            }

            public async Task<string> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
            {
                var offerFromRepo = await _offerRepository.FindByIdAsync(request.OfferId);
                if (offerFromRepo == null) throw new OfferNotFoundException(request.OfferId);

                offerFromRepo.UpdateOffer(request.Name, request.PhotoUrl, request.StartDate, request.EndDate);

                _offerRepository.Update(offerFromRepo);

                await _offerRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return offerFromRepo.Id.ToString();
            }
        }
    }
}

using Domain.OffersManagment.AggregatesModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OffersManagment.Commands.CreateOffer
{
    public class CreateOfferCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Order { get; set; }

        public class Handler : IRequestHandler<CreateOfferCommand, string>
        {
            private readonly IOfferRepository _offerRepository;

            public Handler(IOfferRepository offerRepository)
            {
                _offerRepository = offerRepository;
            }

            public async Task<string> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
            {

                var newOfferToAdd = new Offer(request.Name, request.PhotoUrl, request.StartDate, request.EndDate);

                _offerRepository.Add(newOfferToAdd);

                await _offerRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return newOfferToAdd.Id.ToString();
            }
        }
    }
}

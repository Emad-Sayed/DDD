using Application.OffersManagment.ViewModels;
using Domain.OffersManagment.AggregatesModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OffersManagment.Commands.ReOrderOffers
{
    public class ReOrderOffersCommand : IRequest
    {

        public List<ReOderOffersVM> OrderOffersModel { get; set; }

        public class Handler : IRequestHandler<ReOrderOffersCommand>
        {
            private readonly IOfferRepository _offerRepository;

            public Handler(IOfferRepository offerRepository)
            {
                _offerRepository = offerRepository;
            }

            public async Task<MediatR.Unit> Handle(ReOrderOffersCommand request, CancellationToken cancellationToken)
            {
                var offersIds = request.OrderOffersModel.Select(x => x.OfferId).ToList();
                var offersFromRepo = await _offerRepository.FindByIdsAsync(offersIds);

                foreach (var offerId in offersIds)
                {
                    var offer = offersFromRepo.FirstOrDefault(x => x.Id == new Guid(offerId));
                    var order = request.OrderOffersModel.FirstOrDefault(x => x.OfferId == offerId).Order;
                    offer.ChangeOrder(order);
                }

                _offerRepository.UpdateAll(offersFromRepo);

                await _offerRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return MediatR.Unit.Value;
            }
        }
    }
}

using Domain.OffersManagment.AggregatesModel;
using Domain.OffersManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OffersManagment.Commands.RemoveProductFromOffer
{
    public class RemoveProductFromOfferCommand : IRequest<string>
    {
        public string OfferId { get; set; }
        public string ProductId { get; set; }

        public class Handler : IRequestHandler<RemoveProductFromOfferCommand, string>
        {
            private readonly IOfferRepository _offerRepository;
            private readonly IMediator _mediator;

            public Handler(IOfferRepository offerRepository, IMediator mediator)
            {
                _offerRepository = offerRepository;
                _mediator = mediator;
            }

            public async Task<string> Handle(RemoveProductFromOfferCommand request, CancellationToken cancellationToken)
            {
                var offerFromRepo = await _offerRepository.FindByIdAsync(request.OfferId);
                if (offerFromRepo == null) throw new OfferNotFoundException(request.OfferId);

                var productToRemove = offerFromRepo.Products.FirstOrDefault(x => x.ProductId == request.ProductId);
                if (productToRemove == null) throw new ProductNotFoundInOfferException(request.ProductId);

                offerFromRepo.RemoveProductFromOffer(productToRemove);

                await _offerRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return offerFromRepo.Id.ToString();
            }
        }
    }
}

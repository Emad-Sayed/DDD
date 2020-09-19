using Application.ProductCatalog.ProductAggregate.Queries.ProductById;
using Domain.OffersManagment.AggregatesModel;
using Domain.OffersManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OffersManagment.Commands.AddProductToOffer
{
    public class AddProductToOfferCommand : IRequest<string>
    {
        public string OfferId { get; set; }
        public string ProductId { get; set; }

        public class Handler : IRequestHandler<AddProductToOfferCommand, string>
        {
            private readonly IOfferRepository _offerRepository;
            private readonly IMediator _mediator;

            public Handler(IOfferRepository offerRepository, IMediator mediator)
            {
                _offerRepository = offerRepository;
                _mediator = mediator;
            }

            public async Task<string> Handle(AddProductToOfferCommand request, CancellationToken cancellationToken)
            {
                var offerFromRepo = await _offerRepository.FindByIdAsync(request.OfferId);
                if (offerFromRepo == null) throw new OfferNotFoundException(request.OfferId);

                var productVM = await _mediator.Send(new ProductByIdQuery { ProductId = request.ProductId }, cancellationToken);

                var product = offerFromRepo.AddProductToOffer(productVM.Id, productVM.Name, productVM.Barcode, productVM.PhotoUrl, productVM.AvailableToSell, productVM.Brand.Name, productVM.ProductCategory.Name);

                foreach (var unit in productVM.Units)
                {
                    product.AddUnit(unit.Id, unit.Name, unit.Count, unit.ContentCount, unit.Price, unit.SellingPrice, unit.Weight, unit.IsAvailable);
                }

                _offerRepository.Update(offerFromRepo);

                await _offerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return offerFromRepo.Id.ToString();
            }
        }
    }
}

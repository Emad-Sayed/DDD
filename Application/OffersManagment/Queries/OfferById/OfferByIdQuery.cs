using Application.OffersManagment.ViewModels;
using AutoMapper;
using Domain.OffersManagment.AggregatesModel;
using Domain.OffersManagment.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OffersManagment.Queries.OfferById
{
    public class OfferByIdQuery : IRequest<OfferVM>
    {
        public string OfferId { get; set; }

        public class Handler : IRequestHandler<OfferByIdQuery, OfferVM>
        {
            private readonly IOfferRepository _offersRepository;
            private readonly IMapper _mapper;

            public Handler(IOfferRepository offersRepository, IMapper mapper)
            {
                _offersRepository = offersRepository;
                _mapper = mapper;
            }

            public async Task<OfferVM> Handle(OfferByIdQuery request, CancellationToken cancellationToken)
            {
                var offerFromRepo = await _offersRepository.FindByIdAsync(request.OfferId);
                if (offerFromRepo == null) throw new OfferNotFoundException(request.OfferId);

                var offerToReturn = _mapper.Map<OfferVM>(offerFromRepo);

                return offerToReturn;
            }
        }
    }
}

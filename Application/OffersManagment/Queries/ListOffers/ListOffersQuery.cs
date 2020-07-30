using Application.Common.Models;
using Application.OffersManagment.ViewModels;
using AutoMapper;
using Domain.OffersManagment.AggregatesModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OffersManagment.Queries.ListOffers
{
    public class ListOffersQuery : IRequest<ListEntityVM<OfferVM>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string KeyWord { get; set; }

        public class Handler : IRequestHandler<ListOffersQuery, ListEntityVM<OfferVM>>
        {
            private readonly IOfferRepository _offersRepository;
            private readonly IMapper _mapper;

            public Handler(IOfferRepository offersRepository, IMapper mapper)
            {
                _offersRepository = offersRepository;
                _mapper = mapper;
            }

            public async Task<ListEntityVM<OfferVM>> Handle(ListOffersQuery request, CancellationToken cancellationToken)
            {
                var offersFromRepo = await _offersRepository.GetAllAsync(request.PageNumber, request.PageSize, request.KeyWord);

                var offersToReturn = _mapper.Map<List<OfferVM>>(offersFromRepo.Item2);

                return new ListEntityVM<OfferVM> { TotalCount = offersFromRepo.Item1, Data = offersToReturn };
            }
        }
    }
}

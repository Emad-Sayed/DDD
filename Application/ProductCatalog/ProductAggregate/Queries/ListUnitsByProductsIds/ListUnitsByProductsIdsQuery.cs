using Application.ProductCatalog.ProductAggregate.ViewModels;
using AutoMapper;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.ProductAggregate.Queries.ListUnitsByProductsIds
{
    public class ListUnitsByProductsIdsQuery : IRequest<List<UnitVM>>
    {
        public List<string> ProductsIds { get; set; } = new List<string>();

        public class Handler : IRequestHandler<ListUnitsByProductsIdsQuery, List<UnitVM>>
        {
            private readonly IProductRepository _productsRepository;
            private readonly IMapper _mapper;

            public Handler(IProductRepository brandRepository, IMapper mapper)
            {
                _productsRepository = brandRepository;
                _mapper = mapper;
            }

            public async Task<List<UnitVM>> Handle(ListUnitsByProductsIdsQuery request, CancellationToken cancellationToken)
            {
                var unitsFromRepo = await _productsRepository.GetProductsUnits(request.ProductsIds);

                var unitsToReturn = _mapper.Map<List<UnitVM>>(unitsFromRepo);

                return unitsToReturn;
            }
        }
    }
}

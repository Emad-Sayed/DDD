using Application.ProductCatalog.ProductAggregate.ViewModels;
using AutoMapper;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.ProductCatalog.Exceptions;

namespace Application.ProductCatalog.ProductAggregate.Queries.ProductById
{
    public class ProductByIdQuery : IRequest<ProductVM>
    {
        public string ProductId { get; set; }

        public class Handler : IRequestHandler<ProductByIdQuery, ProductVM>
        {
            private readonly IProductRepository _brandRepository;
            private readonly IMapper _mapper;

            public Handler(IProductRepository brandRepository, IMapper mapper)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
            }

            public async Task<ProductVM> Handle(ProductByIdQuery request, CancellationToken cancellationToken)
            {
                var  productFromRepo = await _brandRepository.FindByIdAsync(request.ProductId);
                if (productFromRepo == null) throw new ProductNotFoundException(request.ProductId);

                var productToReturn = _mapper.Map<ProductVM>(productFromRepo);

                return productToReturn;
            }
        }
    }
}

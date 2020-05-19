﻿using Application.Common.Exceptions;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {

        public string ProductId { get; set; }


        public class Handler : IRequestHandler<DeleteProductCommand>
        {
            private readonly IProductRepository _productRepository;

            public Handler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<MediatR.Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                // get product by id
                var productFromRepo = await _productRepository.FindByIdAsync(request.ProductId);
                if (productFromRepo == null)
                    throw new NotFoundException(nameof(productFromRepo));

                // we call delete product to rase delete product event to sync with algolia
                productFromRepo.DeleteProduct();

                // update product with the new unit deleted
                _productRepository.Delete(productFromRepo);

                // save changes in the database and rase ProductUpdated event
                await _productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return MediatR.Unit.Value;
            }
        }
    }
}

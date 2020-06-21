using Domain.Common.Exceptions;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.ProductAggregate.Commands.DeleteUnit
{
    public class DeleteUnitCommand : IRequest
    {
        public string UnitId { get; set; }

        public string ProductId { get; set; }


        public class Handler : IRequestHandler<DeleteUnitCommand>
        {
            private readonly IProductRepository _productRepository;

            public Handler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<MediatR.Unit> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
            {
                // get product by id
                var productFromRepo = await _productRepository.FindByIdAsync(request.ProductId);
                if (productFromRepo == null) throw new ProductNotFoundException(request.ProductId);

                // delete unit to product
                productFromRepo.DeleteProductUnit(request.UnitId);

                // update product with the new unit deleted
                _productRepository.Update(productFromRepo);

                // save changes in the database and rase ProductUpdated event
                await _productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return MediatR.Unit.Value;
            }
        }
    }
}

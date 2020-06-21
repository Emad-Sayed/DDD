using Domain.Common.Exceptions;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.ProductAggregate.Commands.UpdateUnit
{
    public class UpdateUnitCommand : IRequest
    {
        public string Name { get; set; }

        // How many units from this unit
        public int Count { get; set; }

        // How many item in this unit
        public int ContentCount { get; set; }

        // The price of 1 unit
        public float Price { get; set; }

        // The Selling Price of 1 unit
        public float SellingPrice { get; set; }

        // The Weight of 1 unit
        public float Weight { get; set; }

        // Is this unit enabled and can be used
        public bool IsAvailable { get; set; }

        public string ProductId { get; set; }
        public string Id { get; set; }


        public class Handler : IRequestHandler<UpdateUnitCommand>
        {
            private readonly IProductRepository _productRepository;

            public Handler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<MediatR.Unit> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
            {
                // get product by id
                var productFromRepo = await _productRepository.FindByIdAsync(request.ProductId);
                if (productFromRepo == null) throw new ProductNotFoundException(request.ProductId);

                // update unit to product
                productFromRepo.UpdateProductUnit(request.Id, request.Name, request.Count, request.ContentCount, request.Price, request.SellingPrice, request.Weight, request.IsAvailable);

                // update product with the new unit updated
                _productRepository.Update(productFromRepo);

                // save changes in the database and rase ProductUpdated event
                await _productRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return MediatR.Unit.Value;
            }
        }
    }
}

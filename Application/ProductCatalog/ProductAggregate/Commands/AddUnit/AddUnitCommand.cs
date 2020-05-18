﻿using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.ProductAggregate.Commands.AddUnit
{
    public class AddUnitCommand : IRequest
    {
        public string Name { get; set; }

        // How many units from this unit
        public int Count { get; set; }

        // How many item in this unit
        public int ContentCount { get; set; }

        // The price of 1 unit
        public float Price { get; set; }

        // The Weight of 1 unit
        public float Weight { get; set; }

        // Is this unit enabled and can be used
        public bool IsAvilable { get; set; }

        public string ProductId { get; set; }


        public class Handler : IRequestHandler<AddUnitCommand>
        {
            private readonly IProductRepository _productRepository;

            public Handler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<MediatR.Unit> Handle(AddUnitCommand request, CancellationToken cancellationToken)
            {
                // get product by id
                var productFromRepo = await _productRepository.FindByIdAsync(request.ProductId);

                // add unit to product
                productFromRepo.AddUnitToProduct(request.Name, request.Count, request.ContentCount, request.Price, request.Weight, request.IsAvilable);

                // update product with the new unit created
                _productRepository.Update(productFromRepo);

                // save changes in the database and rase ProductUpdated event
                await _productRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return MediatR.Unit.Value;
            }
        }
    }
}

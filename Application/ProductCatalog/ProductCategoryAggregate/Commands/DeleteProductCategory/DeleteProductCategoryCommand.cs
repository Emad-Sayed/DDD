using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using Domain.ProductCatalog.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.ProductCategoryAggregate.Commands.DeleteProductCategory
{
    public class DeleteProductCategoryCommand : IRequest
    {
        public string ProductCategoryId { get; set; }

        public class Handler : IRequestHandler<DeleteProductCategoryCommand>
        {
            private readonly IProductCategoryRepository _productCategoryRepository;

            public Handler(IProductCategoryRepository productCategoryRepository)
            {
                _productCategoryRepository = productCategoryRepository;
            }

            public async Task<MediatR.Unit> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
            {
                // get productCategory by id
                var productCategoryFromRepo = await _productCategoryRepository.FindByIdAsync(request.ProductCategoryId);
                if (productCategoryFromRepo == null) throw new ProductCategoryNotFoundException(request.ProductCategoryId);

                if (productCategoryFromRepo.Products.Count > 0) throw new ProductCategoryContainsProductsException(request.ProductCategoryId);

                // we call delete productCategory to rase delete productCategory event to sync with algolia
                productCategoryFromRepo.Delete();

                // update productCategory with the new unit deleted
                _productCategoryRepository.Delete(productCategoryFromRepo);

                // save changes in the database and rase ProductCategoryUpdated event
                await _productCategoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return MediatR.Unit.Value;
            }
        }
    }
}

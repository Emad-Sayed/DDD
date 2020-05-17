using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.ProductCategoryAggregate.Commands.CreateProductCategory
{
    public class CreateProductCategoryCommand : IRequest
    {
        public string Name { get; set; }

        public class Handler : IRequestHandler<CreateProductCategoryCommand>
        {
            private readonly IProductCategoryRepository _productCategoryRepository;

            public Handler(IProductCategoryRepository productCategoryRepository)
            {
                _productCategoryRepository = productCategoryRepository;
            }

            public async Task<Unit> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
            {
                var newBrandToAdd = new ProductCategory(request.Name);

                _productCategoryRepository.Add(newBrandToAdd);

                await _productCategoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}

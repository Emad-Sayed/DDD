using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.ProductCategoryAggregate.Commands.CreateProductCategory
{
    public class CreateProductCategoryCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string PhotoUrl { get; set; }

        public class Handler : IRequestHandler<CreateProductCategoryCommand, string>
        {
            private readonly IProductCategoryRepository _productCategoryRepository;

            public Handler(IProductCategoryRepository productCategoryRepository)
            {
                _productCategoryRepository = productCategoryRepository;
            }

            public async Task<string> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
            {
                var newProductCategoryToAdd = new ProductCategory(request.Name, request.PhotoUrl);

                _productCategoryRepository.Add(newProductCategoryToAdd);

                await _productCategoryRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return newProductCategoryToAdd.Id.ToString();
            }
        }
    }
}

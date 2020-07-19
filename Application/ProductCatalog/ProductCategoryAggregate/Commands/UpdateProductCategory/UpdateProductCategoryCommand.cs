using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using Domain.ProductCatalog.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.ProductCategoryAggregate.Commands.UpdateProductCategory
{
    public class UpdateProductCategoryCommand : IRequest<string>
    {
        public string ProductCategoryId { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }


        public class Handler : IRequestHandler<UpdateProductCategoryCommand, string>
        {
            private readonly IProductCategoryRepository _productCategoryRepository;

            public Handler(IProductCategoryRepository productCategoryRepository)
            {
                _productCategoryRepository = productCategoryRepository;
            }

            public async Task<string> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
            {
                var productCategoryFromRepo = await _productCategoryRepository.FindByIdAsync(request.ProductCategoryId);
                if (productCategoryFromRepo == null) throw new ProductCategoryNotFoundException(request.ProductCategoryId);

                productCategoryFromRepo.Update(request.Name, request.PhotoUrl);

                _productCategoryRepository.Update(productCategoryFromRepo);

                await _productCategoryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return productCategoryFromRepo.Id.ToString();
            }
        }
    }
}

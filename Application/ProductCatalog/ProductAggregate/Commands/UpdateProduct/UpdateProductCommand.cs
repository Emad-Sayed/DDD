using System.Threading;
using System.Threading.Tasks;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.Exceptions;
using MediatR;

namespace Application.ProductCatalog.ProductAggregate.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<string>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string PhotoUrl { get; set; }
        public string BrandId { get; set; }
        public string ProductCategoryId { get; set; }
        public bool AvailableToSell { get; set; }

        public class Handler : IRequestHandler<UpdateProductCommand, string>
        {
            private readonly IProductRepository _productRepository;

            public Handler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<string> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var productFromRepo = await _productRepository.FindByIdAsync(request.Id);
                if (productFromRepo == null) throw new ProductNotFoundException(request.Id);

                productFromRepo.UpdateProduct(request.Name, request.Barcode, request.PhotoUrl, request.AvailableToSell, request.BrandId, request.ProductCategoryId);

                _productRepository.Update(productFromRepo);

                await _productRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return productFromRepo.Id.ToString();
            }
        }
    }
}

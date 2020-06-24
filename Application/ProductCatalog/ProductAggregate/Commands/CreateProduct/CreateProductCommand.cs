using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.ProductAggregate.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string PhotoUrl { get; set; }
        public string BrandId { get; set; }
        public string ProductCategoryId { get; set; }
        public bool AvailableToSell { get; set; }

        public class Handler : IRequestHandler<CreateProductCommand, string>
        {
            private readonly IProductRepository _productRepository;

            public Handler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var brand = await _productRepository.GetBrandById(request.BrandId);
                if (brand == null) throw new BrandNotFoundException(request.BrandId);

                var productCategory = await _productRepository.GetProductCategoryById(request.ProductCategoryId);
                if (productCategory == null) throw new ProductCategoryNotFoundException(request.ProductCategoryId);

                var newProductToAdd = new Product(request.Name, request.Barcode, request.PhotoUrl, request.AvailableToSell, request.BrandId, request.ProductCategoryId);

                 _productRepository.Add(newProductToAdd);

                await _productRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return newProductToAdd.Id.ToString();
            }
        }
    }
}

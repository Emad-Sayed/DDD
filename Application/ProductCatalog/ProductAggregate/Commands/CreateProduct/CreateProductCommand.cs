using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.ProductAggregate.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string PhotoUrl { get; set; }
        public string BrandId { get; set; }
        public string ProductCategoryId { get; set; }
        public bool AvailableToSell { get; set; }

        public class Handler : IRequestHandler<CreateProductCommand>
        {
            private readonly IProductRepository _productRepository;

            public Handler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<MediatR.Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var newProductToAdd = new Product(request.Name, request.Barcode, request.PhotoUrl, request.AvailableToSell, request.BrandId, request.ProductCategoryId);

                 _productRepository.Add(newProductToAdd);

                await _productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return MediatR.Unit.Value;
            }
        }
    }
}

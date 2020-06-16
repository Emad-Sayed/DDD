using Application.Common.Exceptions;
using Domain.Common.Exceptions;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string PhotoUrl { get; set; }
        public string BrandId { get; set; }
        public string ProductCategoryId { get; set; }
        public bool AvailableToSell { get; set; }

        public class Handler : IRequestHandler<UpdateProductCommand>
        {
            private readonly IProductRepository _productRepository;

            public Handler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<MediatR.Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var productFromRepo = await _productRepository.FindByIdAsync(request.Id);
                if (productFromRepo == null) throw new RestException(System.Net.HttpStatusCode.BadRequest, new { Product = $"Product with id {request.Id} not found ", code = "product_notfound" });

                productFromRepo.UpdateProduct(request.Name, request.Barcode, request.PhotoUrl, request.AvailableToSell, request.BrandId, request.ProductCategoryId);

                _productRepository.Update(productFromRepo);

                await _productRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return MediatR.Unit.Value;
            }
        }
    }
}

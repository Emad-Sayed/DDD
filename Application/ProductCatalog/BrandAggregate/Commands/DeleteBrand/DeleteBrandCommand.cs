using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.BrandAggregate.Commands.DeleteBrand
{
    public class DeleteBrandCommand : IRequest
    {
        public string BrandId { get; set; }

        public class Handler : IRequestHandler<DeleteBrandCommand>
        {
            private readonly IBrandRepository _brandRepository;

            public Handler(IBrandRepository brandRepository)
            {
                _brandRepository = brandRepository;
            }

            public async Task<MediatR.Unit> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
            {
                // get brand by id
                var brandFromRepo = await _brandRepository.FindByIdAsync(request.BrandId);
                if (brandFromRepo == null) throw new BrandNotFoundException(request.BrandId);

                if (brandFromRepo.Products.Count > 0) throw new BrandContainsProductsException(request.BrandId);

                // we call delete brand to rase delete brand event to sync with algolia
                brandFromRepo.Delete();

                // update brand with the new unit deleted
                _brandRepository.Delete(brandFromRepo);

                // save changes in the database and rase BrandUpdated event
                await _brandRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return MediatR.Unit.Value;
            }
        }
    }
}

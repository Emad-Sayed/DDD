using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.BrandAggregate.Commands.UpdateBrand
{
    public class UpdateBrandCommand : IRequest<string>
    {
        public string BrandId { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }


        public class Handler : IRequestHandler<UpdateBrandCommand, string>
        {
            private readonly IBrandRepository _brandRepository;

            public Handler(IBrandRepository brandRepository)
            {
                _brandRepository = brandRepository;
            }

            public async Task<string> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
            {
                var brandFromRepo = await _brandRepository.FindByIdAsync(request.BrandId);
                if (brandFromRepo == null) throw new BrandNotFoundException(request.BrandId);

                brandFromRepo.Update(request.Name, request.PhotoUrl);

                _brandRepository.Update(brandFromRepo);

                await _brandRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return brandFromRepo.Id.ToString();
            }
        }
    }
}

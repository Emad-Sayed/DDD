using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.BrandAggregate.Commands.CreateBrand
{
    public class CreateBrandCommand : IRequest
    {
        public string Name { get; set; }

        public class Handler : IRequestHandler<CreateBrandCommand>
        {
            private readonly IBrandRepository _brandRepository;

            public Handler(IBrandRepository brandRepository)
            {
                _brandRepository = brandRepository;
            }

            public async Task<Unit> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                var newBrandToAdd = new Brand(request.Name);

                _brandRepository.Add(newBrandToAdd);

                await _brandRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}

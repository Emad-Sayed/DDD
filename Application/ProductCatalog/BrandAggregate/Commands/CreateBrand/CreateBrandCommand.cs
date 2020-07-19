using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.BrandAggregate.Commands.CreateBrand
{
    public class CreateBrandCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string PhotoUrl { get; set; }

        public class Handler : IRequestHandler<CreateBrandCommand, string>
        {
            private readonly IBrandRepository _brandRepository;

            public Handler(IBrandRepository brandRepository)
            {
                _brandRepository = brandRepository;
            }

            public async Task<string> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                var newBrandToAdd = new Brand(request.Name, request.PhotoUrl);

                _brandRepository.Add(newBrandToAdd);

                await _brandRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);

                return newBrandToAdd.Id.ToString();
            }
        }
    }
}

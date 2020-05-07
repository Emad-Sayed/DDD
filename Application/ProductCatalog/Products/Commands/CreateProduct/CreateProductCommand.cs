using Domain.ProductCatalog.Product.DomainModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductCatalog.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest
    {

        public class Handler : IRequestHandler<CreateProductCommand>
        {
            private readonly IProductCatalogContext _context;
            private readonly IMediator _mediator;

            public Handler(IProductCatalogContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var entity = new Product
                {
                    
                };

                _context.Products.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                await _mediator.Publish(new ProductCreated { }, cancellationToken);

                return Unit.Value;
            }
        }
    }
}

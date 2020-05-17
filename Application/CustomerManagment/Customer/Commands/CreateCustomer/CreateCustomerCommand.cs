using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.CustomerManagment.Customer.DomainModels;
namespace Application.CustomerManagment.Customer.Commands.CreateCustomer
{

    public class CreateCustomerCommand : IRequest<string>
    {
        public string AccountId { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string LocationOnMap { get; set; }

        //public class Handler : IRequestHandler<CreateCustomerCommand, string>
        //{
        //    private readonly ICustomerManagmentContext _context;
        //    private readonly IMediator _mediator;

        //    public Handler(ICustomerManagmentContext context, IMediator mediator)
        //    {
        //        _context = context;
        //        _mediator = mediator;
        //    }

        //    public async Task<string> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        //    {
        //        var entity = new Domain.CustomerManagment.Customer.DomainModels.Customer
        //            (
        //            request.AccountId,
        //            request.ShopName,
        //            request.ShopAddress,
        //            request.LocationOnMap
        //            );

        //        _context.Customers.Add(entity);

        //        await _context.SaveChangesAsync(cancellationToken);

        //        await _mediator.Publish(new CustomerCreated { CustomerId = entity.Id.ToString() }, cancellationToken);
        //        return entity.Id.ToString();
        //    }
        //}
    }

}

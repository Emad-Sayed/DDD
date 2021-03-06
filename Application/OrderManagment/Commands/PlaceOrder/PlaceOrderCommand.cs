using Application.Common.Interfaces;
using Application.CustomerManagment.Queries.CustomerByAccountId;
using Application.DistributorManagment.Queries.DistributorById;
using Application.OrderManagment.ViewModels;
using Application.ProductCatalog.ProductAggregate.Queries.ProductById;
using Application.ShoppingVan.Queries.CurrentCustomerVan;
using Domain.CustomerManagment.Exceptions;
using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using Domain.ShoppingVan.Exceptions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Application.OrderManagment.Commands.PlaceOrder
{
    public class PlaceOrderCommand : IRequest<string>
    {
        public List<VanItemVM> Items { get; set; }
        public class Handler : IRequestHandler<PlaceOrderCommand, string>
        {
            private readonly IMediator _mediator;
            private readonly ICurrentUserService _currentUserService;
            private readonly IOrderRepository _orderRepository;

            public Handler(
                IMediator mediator,
                ICurrentUserService currentUserService,
                IOrderRepository orderRepository)
            {
                _mediator = mediator;
                _currentUserService = currentUserService;
                _orderRepository = orderRepository;
            }

            public async Task<string> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
            {
                // Get Current Open shopping van for the current logged in customer
                //var customerVanFromQuery = await _mediator.Send(new CurrentCustomerVanQuery(), cancellationToken);
                //if (customerVanFromQuery == null) throw new EmptyShoppingVanException();


                // Get Current Customer form the current logged in customer
                var customerDetailsFromQuery = await _mediator.Send(new CustomerByAccountIdQuery { AccountId = _currentUserService.UserId }, cancellationToken);
                if (customerDetailsFromQuery == null) throw new CustomerNotFoundException(_currentUserService.UserId);


                List<Order> ordersList = new List<Order>();

                foreach (var vanItem in request.Items)
                {
                    if (vanItem.CustomerCount > 0)
                    {
                        var productDetails = await _mediator.Send(new ProductByIdQuery { ProductId = vanItem.ProductId });

                        var distributordDetails = await _mediator.Send(new DistributorByIdQuery { DistributorId = productDetails.DistributorId });

                        var unit = productDetails.Units.FirstOrDefault(x => x.Id == vanItem.UnitId);

                        if (unit == null) throw new UnitNotFoundException(vanItem.UnitId);

                        var order = ordersList.FirstOrDefault(x => x.DistributorId == productDetails.DistributorId);
                        if (order == null)
                        {
                            order = new Order(
                                productDetails.DistributorId,
                                distributordDetails.Name,
                                _currentUserService.UserId,
                                customerDetailsFromQuery.FullName,
                                customerDetailsFromQuery.CustomerCode,
                                customerDetailsFromQuery.ShopName,
                                customerDetailsFromQuery.ShopAddress,
                                customerDetailsFromQuery.Area.City.Name,
                                customerDetailsFromQuery.Area.Name,
                                customerDetailsFromQuery.LocationOnMap);
                        }

                        order.AddOrderItem(productDetails.Id, productDetails.Name, productDetails.Brand.Name, unit.Price, unit.SellingPrice, productDetails.PhotoUrl, unit.Id, unit.Name, vanItem.CustomerCount);

                        _orderRepository.Add(order);

                        await _orderRepository.UnitOfWork.SaveEntitiesSeveralTransactionsAsync(cancellationToken);
                    }

                }




                return "orderToPlace.Id.ToString()";
            }
        }
    }
}

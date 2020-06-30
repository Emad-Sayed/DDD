using Application.Common.Validators;
using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.OrderManagment.Queries.CustomerOrders
{
    public class CustomerOrdersQueryValidators : AbstractValidator<CustomerOrdersQuery>
    {
        public CustomerOrdersQueryValidators()
        {
            RuleFor(x => x.CustomerId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad OrderId Format OrderId must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.OrderStatuses).Cascade(CascadeMode.StopOnFirstFailure).NotNull().Must(BetweenZeroToFour).WithMessage("Order State Must be between 0 to 4").WithErrorCode("invalid_order_status");
        }

        private bool BetweenZeroToFour(List<OrderStatus> OrderStatus)
        {
            return OrderStatus.All(orderStatus => orderStatus >= Domain.OrderManagment.AggregatesModel.OrderAggregate.OrderStatus.Placed && orderStatus <= Domain.OrderManagment.AggregatesModel.OrderAggregate.OrderStatus.Cancelled);
        }
    }
}

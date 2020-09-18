using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OrderManagment.Commands.PlaceOrder
{
    public class PlaceOrderCommandValidator : AbstractValidator<PlaceOrderCommand>
    {
        public PlaceOrderCommandValidator()
        {
            RuleFor(x => x.Items).NotEmpty().WithMessage("Order items must be not empt").WithErrorCode("order_items_must_be_not_empty");
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OrderManagment.Commands.ShippOrder
{
    public class ShippOrderCommandValidator : AbstractValidator<ShippOrderCommand>
    {
        public ShippOrderCommandValidator()
        {
        }
    }
}

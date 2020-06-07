using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OrderManagment.Commands.CancelOrder
{
    public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderCommandValidator()
        {
        }
    }
}

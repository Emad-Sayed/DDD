using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OrderManagment.Commands.ConfirmOrder
{
    public class ConfirmOrderCommandValidator : AbstractValidator<ConfirmOrderCommand>
    {
        public ConfirmOrderCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
        }
    }
}

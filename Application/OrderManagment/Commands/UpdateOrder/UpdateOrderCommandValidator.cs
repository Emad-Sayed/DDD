using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OrderManagment.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
            RuleFor(x => x.OrderItemId).NotEmpty();
            RuleFor(x => x.UnitName).NotEmpty();
            RuleFor(x => x.UnitId).NotEmpty();
            RuleFor(x => x.UnitCount).NotEmpty();
        }
    }
}

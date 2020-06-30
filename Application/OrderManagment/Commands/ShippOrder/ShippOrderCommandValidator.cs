using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Validators;

namespace Application.OrderManagment.Commands.ShippOrder
{
    public class ShippOrderCommandValidator : AbstractValidator<ShippOrderCommand>
    {
        public ShippOrderCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad OrderId Format OrderId must be GUID").WithErrorCode("invalid_guid");
        }
    }
}

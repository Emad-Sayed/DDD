using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Validators;

namespace Application.OrderManagment.Commands.CancelOrder
{
    public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad OrderId Format OrderId must be GUID").WithErrorCode("invalid_guid");
        }
    }
}

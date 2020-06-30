using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Validators;

namespace Application.OrderManagment.Commands.ConfirmOrder
{
    public class ConfirmOrderCommandValidator : AbstractValidator<ConfirmOrderCommand>
    {
        public ConfirmOrderCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad OrderId Format OrderId must be GUID").WithErrorCode("invalid_guid");
        }
    }
}

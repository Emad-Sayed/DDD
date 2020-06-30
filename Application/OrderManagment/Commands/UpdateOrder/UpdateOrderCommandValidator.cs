using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Validators;

namespace Application.OrderManagment.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad OrderId Format OrderId must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.OrderItemId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad OrderItemId Format OrderItemId must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.UnitId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad UnitId Format UnitId must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.UnitName).NotEmpty().WithMessage("Unit name must be not Empty").WithErrorCode("unit_name_empty");
            RuleFor(x => x.UnitCount).GreaterThan(0).WithMessage("Unit count must be greater than zero").WithErrorCode("must_be_grater_than_zero");
        }
    }
}

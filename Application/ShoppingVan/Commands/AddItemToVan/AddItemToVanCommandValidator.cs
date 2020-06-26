using Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ShoppingVan.Commands.AddItemToVan
{
    public class AddItemToVanCommandValidator : AbstractValidator<AddItemToVanCommand>
    {
        public AddItemToVanCommandValidator()
        {
            RuleFor(x => x.ProductId).Must(GuidValidator.IsGuid).WithMessage("Bad ProductId Format ProductId must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.UnitId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad UnitId Format UnitId must be GUID").WithErrorCode("invalid_guid");
        }
    }
}

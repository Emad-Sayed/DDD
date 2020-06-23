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
            RuleFor(x => x.ProductId).Must(guid => GuidValidator.IsGuid(guid)).WithMessage("Bad ProductId Format ProductId must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.UnitId).NotEmpty().Must(guid => GuidValidator.IsGuid(guid)).WithMessage("Bad UnitId Format UnitId must be GUID").WithErrorCode("invalid_guid");
        }
    }
}

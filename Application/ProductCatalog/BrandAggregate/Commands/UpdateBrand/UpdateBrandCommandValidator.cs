using Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.BrandAggregate.Commands.UpdateBrand
{
    public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator()
        {
            RuleFor(x => x.BrandId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad BrandId Format Id must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Brand name must be not Empty").WithErrorCode("name_empty");
        }
    }
}

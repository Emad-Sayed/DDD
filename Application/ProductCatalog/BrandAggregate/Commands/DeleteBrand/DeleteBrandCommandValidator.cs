using Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.BrandAggregate.Commands.DeleteBrand
{
    public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
    {
        public DeleteBrandCommandValidator()
        {
            RuleFor(x => x.BrandId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad BrandId Format Id must be GUID").WithErrorCode("invalid_guid");
        }
    }
}

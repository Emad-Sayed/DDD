using Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductCategoryAggregate.Commands.UpdateProductCategory
{
    public class UpdateProductCategoryCommandValidator : AbstractValidator<UpdateProductCategoryCommand>
    {
        public UpdateProductCategoryCommandValidator()
        {
            RuleFor(x => x.ProductCategoryId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad ProductCategoryId Format Id must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.Name).NotEmpty().WithMessage("ProductCategory name must be not Empty").WithErrorCode("name_empty");
        }
    }
}

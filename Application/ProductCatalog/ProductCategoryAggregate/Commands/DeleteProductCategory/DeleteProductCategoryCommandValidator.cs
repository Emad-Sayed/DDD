using Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductCategoryAggregate.Commands.DeleteProductCategory
{
    public class DeleteProductCategoryCommandValidator : AbstractValidator<DeleteProductCategoryCommand>
    {
        public DeleteProductCategoryCommandValidator()
        {
            RuleFor(x => x.ProductCategoryId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad ProductCategoryId Format Id must be GUID").WithErrorCode("invalid_guid");
        }
    }
}

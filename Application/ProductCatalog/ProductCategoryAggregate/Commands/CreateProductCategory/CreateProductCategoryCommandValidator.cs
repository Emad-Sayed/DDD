using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductCategoryAggregate.Commands.CreateProductCategory
{
    public class CreateProductCategoryCommandValidator : AbstractValidator<CreateProductCategoryCommand>
    {
        public CreateProductCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product Category name must be not Empty").WithErrorCode("name_empty");
        }
    }
}

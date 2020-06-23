using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.BrandAggregate.Commands.CreateBrand
{
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Brand name must be not Empty").WithErrorCode("name_empty");
        }
    }
}

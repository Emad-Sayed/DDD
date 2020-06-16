using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.BrandId).NotEmpty();
            RuleFor(x => x.ProductCategoryId).NotEmpty();
            RuleFor(x => x.AvailableToSell).NotEmpty();
        }
    }
}

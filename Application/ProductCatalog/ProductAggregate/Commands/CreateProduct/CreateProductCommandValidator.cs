using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductAggregate.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Barcode).NotEmpty();
            RuleFor(x => x.PhotoUrl).NotEmpty();
            RuleFor(x => x.BrandId).NotEmpty();
            RuleFor(x => x.ProductCategoryId).NotEmpty();
        }
    }
}

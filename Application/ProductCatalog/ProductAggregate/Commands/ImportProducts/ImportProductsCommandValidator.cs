using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductAggregate.Commands.ImportProducts
{
    public class ImportProductsCommandValidator : AbstractValidator<ImportProductsCommand>
    {
        public ImportProductsCommandValidator()
        {
            RuleFor(x => x.Products).NotEmpty().WithMessage("Products must be not Empty").WithErrorCode("empty_products");
        }
    }
}

using Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.ProductCatalog.ProductAggregate.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name must be not Empty").WithErrorCode("name_empty");
            RuleFor(x => x.Barcode).NotEmpty().WithMessage("Bracode must be not Empty").WithErrorCode("barcode_empty");
            RuleFor(x => x.BrandId).NotEmpty().Must(guid => GuidValidator.IsGuid(guid)).WithMessage("Bad BrandId Format BrandId must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.ProductCategoryId).Must(guid => GuidValidator.IsGuid(guid)).WithMessage("Bad ProductCategoryId Format ProductCategoryId must be GUID").WithErrorCode("invalid_guid");
        }
    }
}

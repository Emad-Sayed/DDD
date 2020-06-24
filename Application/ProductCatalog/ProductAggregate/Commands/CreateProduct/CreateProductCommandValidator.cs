using Application.Common.Validators;
using FluentValidation;

namespace Application.ProductCatalog.ProductAggregate.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name must be not Empty").WithErrorCode("name_empty");
            RuleFor(x => x.Barcode).NotEmpty().WithMessage("Bracode must be not Empty").WithErrorCode("barcode_empty");
            RuleFor(x => x.BrandId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad BrandId Format BrandId must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.ProductCategoryId).Must(GuidValidator.IsGuid).WithMessage("Bad ProductCategoryId Format ProductCategoryId must be GUID").WithErrorCode("invalid_guid");
        }
    }
}

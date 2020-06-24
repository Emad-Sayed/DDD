using Application.Common.Validators;
using FluentValidation;

namespace Application.ProductCatalog.ProductAggregate.Commands.DeleteProduct
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad ProductId Format Id must be GUID").WithErrorCode("invalid_guid");
        }

    }
}
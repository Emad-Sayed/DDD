using Application.Common.Validators;
using FluentValidation;

namespace Application.ProductCatalog.ProductAggregate.Commands.DeleteUnit
{
    public class DeleteUnitCommandValidator : AbstractValidator<DeleteUnitCommand>
    {
        public DeleteUnitCommandValidator()
        {
            RuleFor(x => x.UnitId).NotEmpty().NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad UnitId Format UnitId must be GUID").WithErrorCode("invalid_guid"); ;
            RuleFor(x => x.ProductId).NotEmpty().NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad ProductId Format ProductId must be GUID").WithErrorCode("invalid_guid"); ;
        }
    }
}

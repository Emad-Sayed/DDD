using Application.Common.Validators;
using FluentValidation;

namespace Application.ProductCatalog.ProductAggregate.Commands.UpdateUnit
{
    public class UpdateUnitCommandValidator : AbstractValidator<UpdateUnitCommand>
    {
        public UpdateUnitCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad UnitId Format UnitId must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Unit name must be not Empty").WithErrorCode("name_empty");
            RuleFor(x => x.Count).GreaterThan(0).WithMessage("Unit count must be greater than zero").WithErrorCode("must_be_grater_than_zero");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Unit Price must be greater than zero").WithErrorCode("must_be_grater_than_zero");
            RuleFor(x => x.ContentCount).GreaterThan(0).WithMessage("Unit Content must be greater than zero").WithErrorCode("must_be_grater_than_zero");
            RuleFor(x => x.SellingPrice).GreaterThan(0).WithMessage("Unit Selling Price must be greater than zero").WithErrorCode("must_be_grater_than_zero");
            RuleFor(x => x.ProductId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad ProductId Format ProductId must be GUID").WithErrorCode("invalid_guid");
        }
    }
}

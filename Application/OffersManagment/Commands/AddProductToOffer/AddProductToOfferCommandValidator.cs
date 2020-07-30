using Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OffersManagment.Commands.AddProductToOffer
{
    public class AddProductToOfferCommandValidator : AbstractValidator<AddProductToOfferCommand>
    {
        public AddProductToOfferCommandValidator()
        {
            RuleFor(x => x.OfferId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad OfferId Format OfferId must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.ProductId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad ProductId Format ProductId must be GUID").WithErrorCode("invalid_guid");
        }
    }
}

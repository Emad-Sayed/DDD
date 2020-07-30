using Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OffersManagment.Commands.UpdateOffer
{
    public class UpdateOfferCommandValidator : AbstractValidator<UpdateOfferCommand>
    {
        public UpdateOfferCommandValidator()
        {
            RuleFor(x => x.OfferId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad OfferId Format OfferId must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Offer name must be not Empty").WithErrorCode("name_empty");
        }
    }
}

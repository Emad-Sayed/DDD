using Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OffersManagment.Commands.DeleteOffer
{
    public class DeleteOfferCommandValidator : AbstractValidator<DeleteOfferCommand>
    {
        public DeleteOfferCommandValidator()
        {
            RuleFor(x => x.OfferId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad OfferId Format Id must be GUID").WithErrorCode("invalid_guid");
        }

    }
}

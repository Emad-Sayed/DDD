using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OffersManagment.Commands.CreateOffer
{
    public class CreateOfferCommandValidator : AbstractValidator<CreateOfferCommand>
    {
        public CreateOfferCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Offer name must be not Empty").WithErrorCode("name_empty");
        }
    }
}

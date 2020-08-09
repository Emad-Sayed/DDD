using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OffersManagment.Commands.ReOrderOffers
{
    public class ReOrderOffersCommandValidator : AbstractValidator<ReOrderOffersCommand>
    {
        public ReOrderOffersCommandValidator()
        {
            RuleFor(x => x.OrderOffersModel).NotEmpty();
        }
    }
}

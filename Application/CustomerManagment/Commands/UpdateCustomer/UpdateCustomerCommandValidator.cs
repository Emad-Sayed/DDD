using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Application.CustomerManagment.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.ShopName).NotEmpty();
            RuleFor(x => x.ShopAddress).NotEmpty();
            RuleFor(x => x.LocationOnMap).NotEmpty();
        }
    }
}

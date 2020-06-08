using Application.CustomerManagment.Commands.CreateCustomer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CustomerManagment.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.ShopName).NotEmpty();
            RuleFor(x => x.ShopAddress).NotEmpty();
            RuleFor(x => x.LocationOnMap).NotEmpty();
        }
    }
}

using Application.Common.Validators;
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
            RuleFor(x => x.AccountId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad AccountId Format AccountId must be GUID").WithErrorCode("invalid_guid"); ;
            RuleFor(x => x.ShopName).NotEmpty().WithMessage("Customer ShopName must be not Empty").WithErrorCode("shop_name_empty");
            //RuleFor(x => x.ShopAddress).NotEmpty().WithMessage("Customer ShopAddress must be not Empty").WithErrorCode("shop_address_empty");
            //RuleFor(x => x.LocationOnMap).NotEmpty().WithMessage("Customer LocationOnMap must be not Empty").WithErrorCode("location_on_map_empty");
        }
    }
}

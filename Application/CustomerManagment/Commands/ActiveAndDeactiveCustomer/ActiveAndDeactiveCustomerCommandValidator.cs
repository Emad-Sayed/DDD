using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CustomerManagment.Commands.DeleteCustomer
{
    public class ActiveAndDeactiveCustomerCommandValidator : AbstractValidator<ActiveAndDeactiveCustomerCommand>
    {
        public ActiveAndDeactiveCustomerCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
        }
    }
}

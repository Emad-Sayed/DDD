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
        }
    }
}

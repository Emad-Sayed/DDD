using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DistributorManagment.Commands.CreateDistributorUser
{
    public class CreateDistributorUserCommandValidator : AbstractValidator<CreateDistributorUserCommand>
    {
        public CreateDistributorUserCommandValidator()
        {
            RuleFor(x => x.DistributorId).NotEmpty();
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}

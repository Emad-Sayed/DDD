using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DistributorManagment.Commands.UpdateDistributorUser
{
    public class UpdateDistributorUserCommandValidator : AbstractValidator<UpdateDistributorUserCommand>
    {
        public UpdateDistributorUserCommandValidator()
        {
            RuleFor(x => x.DistributorId).NotEmpty();
            RuleFor(x => x.DistributorUserId).NotEmpty();
            RuleFor(x => x.FullName).NotEmpty();
        }
    }
}

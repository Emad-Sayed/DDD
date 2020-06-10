using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DistributorManagment.Commands.DeleteDistributorUser
{
    public class DeleteDistributorUserCommandValidator : AbstractValidator<DeleteDistributorUserCommand>
    {
        public DeleteDistributorUserCommandValidator()
        {
            RuleFor(x => x.DistributorId).NotEmpty();
            RuleFor(x => x.DistributorUserId).NotEmpty();
        }
    }
}

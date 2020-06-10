using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DistributorManagment.Commands.DeleteDistributor
{
    public class DeleteDistributorCommandValidator : AbstractValidator<DeleteDistributorCommand>
    {
        public DeleteDistributorCommandValidator()
        {
            RuleFor(x => x.DistributorId).NotEmpty();
        }
    }
}

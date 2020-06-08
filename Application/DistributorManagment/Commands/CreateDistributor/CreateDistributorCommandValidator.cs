using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DistributorManagment.Commands.CreateDistributor
{
    public class CreateDistributorCommandValidator : AbstractValidator<CreateDistributorCommand>
    {
        public CreateDistributorCommandValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.LocationOnMap).NotEmpty();
        }
    }
}

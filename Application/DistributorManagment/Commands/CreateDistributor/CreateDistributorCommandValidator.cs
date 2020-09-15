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
            RuleFor(x => x.Name).NotEmpty().WithMessage("Distributor name must be not Empty").WithErrorCode("name_empty");
            RuleFor(x => x.AreasIds).NotEmpty().WithMessage("Distributor AreaIds must be not Empty").WithErrorCode("empty_area");
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DistributorManagment.Commands.CreateCity
{
    public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
    {
        public CreateCityCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("City name must be not Empty").WithErrorCode("name_empty");
        }
    }
}

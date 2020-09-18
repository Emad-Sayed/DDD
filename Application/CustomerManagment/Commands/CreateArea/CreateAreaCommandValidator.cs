using Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CustomerManagment.Commands.CreateArea
{
    public class CreateAreaCommandValidator : AbstractValidator<CreateAreaCommand>
    {
        public CreateAreaCommandValidator()
        {
            RuleFor(x => x.CityId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad CityId Format CityId must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Area name must be not Empty").WithErrorCode("name_empty");
        }
    }
}

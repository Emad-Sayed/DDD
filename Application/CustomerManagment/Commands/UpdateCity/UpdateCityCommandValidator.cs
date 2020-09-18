using Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CustomerManagment.Commands.UpdateCity
{
    public class UpdateCityCommandValidator : AbstractValidator<UpdateCityCommand>
    {
        public UpdateCityCommandValidator()
        {
            RuleFor(x => x.CityId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad CityId Format CityId must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Distributor name must be not Empty").WithErrorCode("name_empty");
        }
    }
}

using Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CustomerManagment.Commands.DeleteCity
{
    public class DeleteCityCommandValidator : AbstractValidator<DeleteCityCommand>
    {
        public DeleteCityCommandValidator()
        {
            RuleFor(x => x.CityId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad CityId Format CityId must be GUID").WithErrorCode("invalid_guid");
        }
    }
}

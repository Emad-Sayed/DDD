using Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CustomerManagment.Commands.DeleteArea
{
    public class DeleteAreaCommandValidator : AbstractValidator<DeleteAreaCommand>
    {
        public DeleteAreaCommandValidator()
        {
            RuleFor(x => x.CityId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad CityId Format CityId must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.AreaId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad AreaId Format AreaId must be GUID").WithErrorCode("invalid_guid");
        }
    }
}

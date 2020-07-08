using Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DistributorManagment.Commands.UpdateDistributor
{
    public class UpdateDistributorCommandValidator : AbstractValidator<UpdateDistributorCommand>
    {
        public UpdateDistributorCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad DistributorId Format DistributorId must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Distributor name must be not Empty").WithErrorCode("name_empty");
            RuleFor(x => x.City).NotEmpty().WithMessage("Distributor City must be not Empty").WithErrorCode("city_empty");
            RuleFor(x => x.Area).NotEmpty().WithMessage("Distributor Area must be not Empty").WithErrorCode("area_empty");
        }
    }
}

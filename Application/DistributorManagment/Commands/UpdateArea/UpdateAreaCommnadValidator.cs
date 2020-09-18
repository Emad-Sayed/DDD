using Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DistributorManagment.Commands.UpdateArea
{
    public class UpdateAreaCommnadValidator : AbstractValidator<UpdateAreaCommnad>
    {
        public UpdateAreaCommnadValidator()
        {
            RuleFor(x => x.CityId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad CityId Format CityId must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.AreaId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad AreaId Format AreaId must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Area name must be not Empty").WithErrorCode("name_empty");
        }
    }
}

using Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DistributorManagment.Commands.UpdateDistributorUser
{
    public class UpdateDistributorUserCommandValidator : AbstractValidator<UpdateDistributorUserCommand>
    {
        public UpdateDistributorUserCommandValidator()
        {
            RuleFor(x => x.DistributorId).NotEmpty().NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad DistributorId Format DistributorId must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.DistributorUserId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad DistributorUserId Format DistributorUserId must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Distributor FullName must be not Empty").WithErrorCode("fullname");
        }
    }
}

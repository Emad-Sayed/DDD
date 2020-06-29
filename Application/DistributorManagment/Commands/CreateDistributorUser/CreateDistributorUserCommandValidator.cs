using Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DistributorManagment.Commands.CreateDistributorUser
{
    public class CreateDistributorUserCommandValidator : AbstractValidator<CreateDistributorUserCommand>
    {
        public CreateDistributorUserCommandValidator()
        {
            RuleFor(x => x.DistributorId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad DistributorId Format DistributorId must be GUID").WithErrorCode("invalid_guid");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Distributor User Full name must be not Empty").WithErrorCode("full_name_empty");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Distributor User Email must be not Empty").WithErrorCode("email_empty");
        }
    }
}

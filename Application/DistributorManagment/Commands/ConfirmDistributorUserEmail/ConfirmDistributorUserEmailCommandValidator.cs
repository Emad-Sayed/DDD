using Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DistributorManagment.Commands.ConfirmDistributorUserEmail
{
    public class ConfirmDistributorUserEmailCommandValidator : AbstractValidator<ConfirmDistributorUserEmailCommand>
    {
        public ConfirmDistributorUserEmailCommandValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad DistributorUserId Format DistributorUserId must be GUID").WithErrorCode("invalid_guid");
        }
    }
}

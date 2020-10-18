using Application.Common.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.NotificationManagment.Commands.ReadNotification
{
    public class ReadNotificationCommandValidator : AbstractValidator<ReadNotificationCommand>
    {
        public ReadNotificationCommandValidator()
        {
            RuleFor(x => x.NotificationId).NotEmpty().Must(GuidValidator.IsGuid).WithMessage("Bad NotificationId Format NotificationId must be GUID").WithErrorCode("invalid_guid");
        }
    }
}

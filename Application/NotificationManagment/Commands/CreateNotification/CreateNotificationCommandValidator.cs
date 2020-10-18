using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.NotificationManagment.Commands.CreateNotification
{
    public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Notification Title must be not Empty").WithErrorCode("title_empty");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Notification Content must be not Empty").WithErrorCode("content_empty");
            RuleFor(x => x.EntityId).NotEmpty().WithMessage("Notification EntityId must be not Empty").WithErrorCode("entity_id_empty");
        }
    }
}

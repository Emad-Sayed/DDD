﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DistributorManagment.Commands.UpdateDistributor
{
    public class UpdateDistributorCommandValidator : AbstractValidator<UpdateDistributorCommand>
    {
        public UpdateDistributorCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
        }
    }
}

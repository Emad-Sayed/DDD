﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductAggregate.Commands.UpdateUnit
{
    public class UpdateUnitCommandValidator : AbstractValidator<UpdateUnitCommand>
    {
        public UpdateUnitCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Count).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.ContentCount).NotEmpty();
            RuleFor(x => x.SellingPrice).NotEmpty();
            RuleFor(x => x.IsAvailable).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}

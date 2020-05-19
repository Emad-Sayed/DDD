using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductAggregate.Commands.UpdateUnit
{
    public class UpdateUnitCommandValidator : AbstractValidator<UpdateUnitCommand>
    {
        public UpdateUnitCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.UnitId).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}

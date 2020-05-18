using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductAggregate.Commands.AddUnit
{
    public class AddUnitCommandValidator : AbstractValidator<AddUnitCommand>
    {
        public AddUnitCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}

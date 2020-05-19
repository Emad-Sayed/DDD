using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductAggregate.Commands.DeleteUnit
{
    public class DeleteUnitCommandValidator : AbstractValidator<DeleteUnitCommand>
    {
        public DeleteUnitCommandValidator()
        {
            RuleFor(x => x.UnitId).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}

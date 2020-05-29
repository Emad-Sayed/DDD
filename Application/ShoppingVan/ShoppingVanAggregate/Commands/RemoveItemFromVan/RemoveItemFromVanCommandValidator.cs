using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ShoppingVanBoundedContext.ShoppingVanAggregate.Commands.RemoveItemFromVan
{
    public class RemoveItemFromVanCommandValidator : AbstractValidator<RemoveItemFromVanCommand>
    {
        public RemoveItemFromVanCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ShoppingVan.Commands.AddItemToVan
{
    public class AddItemToVanCommandValidator : AbstractValidator<AddItemToVanCommand>
    {
        public AddItemToVanCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}

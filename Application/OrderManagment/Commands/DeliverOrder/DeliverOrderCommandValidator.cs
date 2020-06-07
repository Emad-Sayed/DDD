using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OrderManagment.Commands.DeliverOrder
{
    public class DeliverOrderCommandValidator : AbstractValidator<DeliverOrderCommand>
    {
        public DeliverOrderCommandValidator()
        {
        }
    }
}

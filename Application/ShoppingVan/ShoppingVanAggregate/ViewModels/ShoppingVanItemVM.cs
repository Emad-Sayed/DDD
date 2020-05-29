using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ShoppingVanBoundedContext.ShoppingVanAggregate.ViewModels
{
    public class ShoppingVanItemVM
    {
        public string ProductId { get; private set; }
        public int Amount { get; private set; }
    }
}

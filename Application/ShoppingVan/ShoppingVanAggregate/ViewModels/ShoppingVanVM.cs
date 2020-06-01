using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ShoppingVanBoundedContext.ShoppingVanAggregate.ViewModels
{
    public class ShoppingVanVM
    {
        public ICollection<ShoppingVanItemVM> ShoppingVanItems { get; private set; }
        public string CustomerId { get; private set; }
        public int TotalItemsCount { get; private set; }
    }
}

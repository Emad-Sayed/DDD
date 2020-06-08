using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OrderManagment.ViewModels
{
    public class VanVM
    {
        public ICollection<VanItemVM> ShoppingVanItems { get; private set; }
        public string CustomerId { get; private set; }
        public int TotalItemsCount { get; private set; }
    }

}

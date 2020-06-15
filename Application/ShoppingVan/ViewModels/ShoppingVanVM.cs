using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ShoppingVan.ViewModels
{
    public class ShoppingVanVM
    {
        public ICollection<ShoppingVanItemVM> ShoppingVanItems { get; private set; }
        public string CustomerId { get; private set; }
        public int TotalItemsCount { get; private set; }
        public float TotalPrice { get; set; }
    }
}

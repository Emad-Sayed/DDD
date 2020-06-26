using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ShoppingVan.ViewModels
{
    public class ShoppingVanVM
    {
        public ICollection<ShoppingVanItemVM> ShoppingVanItems { get; set; }
        public string CustomerId { get; set; }
        public int TotalItemsCount { get; set; }
        public float TotalPrice { get; set; }
    }
}

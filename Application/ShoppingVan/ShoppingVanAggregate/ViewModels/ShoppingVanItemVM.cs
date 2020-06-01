using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ShoppingVanBoundedContext.ShoppingVanAggregate.ViewModels
{
    public class ShoppingVanItemVM
    {
        public string ProductId { get; set; }
        public string UnitName { get; set; }
        public float UnitPrice { get; set; }
        public string PhotoUrl { get; set; }
        public float SellingPrice { get; set; }
        public int Amount { get; set; }
    }
}

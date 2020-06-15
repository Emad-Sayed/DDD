using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OrderManagment.ViewModels
{
    public class VanVM
    {
        public ICollection<VanItemVM> ShoppingVanItems { get;  set; }
        public string CustomerId { get;  set; }
        public int TotalItemsCount { get;  set; }
        public float TotalPrice { get; set; }
    }

}

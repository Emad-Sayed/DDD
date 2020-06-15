using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OrderManagment.ViewModels
{
    public class OrderItemVM
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string PhotoUrl { get; set; }
        public float UnitPrice { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public int UnitCount { get; set; }

        public string OrderId { get; set; }
    }
}

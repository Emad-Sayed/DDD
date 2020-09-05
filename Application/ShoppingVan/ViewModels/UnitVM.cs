using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ShoppingVan.ViewModels
{
    public class UnitVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float ConsumerPrice { get; set; }
        public float CustomerCount { get; set; }
        public bool IsAvailable { get; set; }
    }
}

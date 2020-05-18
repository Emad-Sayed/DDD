using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductAggregate.ViewModels
{
    public class UnitVM
    {
        public string Id { get; set; }
        public string Unit { get; set; }
        public float Price { get; set; }
        public bool isAvilable { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductAggregate.ViewModels
{
    public class AlgoliaUnitVM
    {
        public string Id { get; set; }
        public string Unit { get; set; }
        public float Price { get; set; }
        public float SellingPrice { get; set; }
        public bool IsAvilable { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductAggregate.ViewModels
{
    public class AlgoliaUnitVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float ConsumerPrice { get; set; }
        public bool IsAvailable { get; set; }
    }
}

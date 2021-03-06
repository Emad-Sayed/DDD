using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductAggregate.ViewModels
{
    public class UnitVM
    {
        public string Id { get; set; }

        public string Name { get; private set; }

        // How many units from this unit
        public int Count { get; private set; }

        // How many item in this unit
        public int ContentCount { get; private set; }

        // The price of 1 unit
        public float Price { get; private set; }

        // The Selling price of 1 unit
        public float SellingPrice { get; set; }

        // The Weight of 1 unit
        public float Weight { get; private set; }

        // Is this unit enabled and can be used
        public bool IsAvailable { get; private set; }

        public string ProductId { get; private set; }

    }
}

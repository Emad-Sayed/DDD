using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ShoppingVan.ViewModels
{
    public class VanPriceInfoVM
    {
        public float TotalVanPriceBeforeTaxValue { get; set; }
        public float TotalVanPriceAfterTaxValue { get; set; }
        public float TaxValue { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OffersManagment.ViewModels
{
    public class ProductVM
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string PhotoUrl { get; set; }
        public bool AvailableToSell { get; set; }
        public List<UnitVM> Units { get; set; }

        public string Brand { get; set; }

        public string ProductCategory { get; set; }
    }
}

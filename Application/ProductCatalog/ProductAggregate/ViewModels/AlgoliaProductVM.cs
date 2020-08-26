using Application.ProductCatalog.BrandAggregate.ViewModels;
using Application.ProductCatalog.ProductCategoryAggregate.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductAggregate.ViewModels
{
    public class AlgoliaProductVM
    {
        public string ProductId { get;  set; }
        public string Name { get;  set; }
        public string Barcode { get;  set; }
        public string PhotoUrl { get;  set; }
        public bool AvailableToSell { get; set; }
        public string ObjectID { get;  set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public List<AlgoliaUnitVM> Units { get; set; }

    }
}

using Application.ProductCatalog.BrandAggregate.ViewModels;
using Application.ProductCatalog.ProductCategoryAggregate.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductAggregate.ViewModels
{
    public class ProductVM
    {
        public string Name { get; private set; }
        public string Barcode { get; private set; }
        public string PhotoUrl { get; private set; }
        public bool AvailableToSell { get; set; }

        public string BrandId { get; private set; }
        public BrandVM Brand { get; private set; }

        public string ProductCategoryId { get; private set; }
        public ProductCategoryVM ProductCategory { get; private set; }

        public ICollection<UnitVM> Units { get; private set; }
    }
}

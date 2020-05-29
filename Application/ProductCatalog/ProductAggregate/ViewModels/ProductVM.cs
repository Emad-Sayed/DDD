using Application.ProductCatalog.BrandAggregate.ViewModels;
using Application.ProductCatalog.ProductCategoryAggregate.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductAggregate.ViewModels
{
    public class ProductVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string PhotoUrl { get; set; }
        public bool AvailableToSell { get; set; }

        public string BrandId { get; set; }
        public BrandVM Brand { get; set; }

        public string ProductCategoryId { get; set; }
        public ProductCategoryVM ProductCategory { get; set; }

        public ICollection<UnitVM> Units { get; set; }
    }
}

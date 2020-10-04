using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductAggregate.ViewModels
{
    public class ProductToImportVM
    {
        public string PhotoUrl { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public BrandToImportVM Brand { get; set; }
        public ProductCategoryToImportVM ProductCategory { get; set; }
        public List<UnitToImportVM> Units { get; set; }
    }

    public class BrandToImportVM
    {
        public string Name { get; set; }
    }

    public class ProductCategoryToImportVM
    {
        public string Name { get; set; }
    }

    public class UnitToImportVM
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public float SellingPrice { get; set; }
        public int Count { get; set; }
    }
}

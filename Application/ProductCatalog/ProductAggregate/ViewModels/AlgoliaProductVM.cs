﻿using Application.ProductCatalog.BrandAggregate.ViewModels;
using Application.ProductCatalog.ProductCategoryAggregate.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductAggregate.ViewModels
{
    public class AlgoliaProductVM
    {
        public string Name { get; private set; }
        public string Barcode { get; private set; }
        public string PhotoUrl { get; private set; }
        public bool AvailableToSell { get; set; }
        public string ObjectID { get; private set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public List<AlgoliaUnitVM> Units { get; set; }

    }
}

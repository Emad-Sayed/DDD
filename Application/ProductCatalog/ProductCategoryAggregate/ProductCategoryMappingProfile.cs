using Application.ProductCatalog.BrandAggregate.ViewModels;
using Application.ProductCatalog.ProductAggregate.ViewModels;
using Application.ProductCatalog.ProductCategoryAggregate.ViewModels;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductCategoryAggregate
{
    public class ProductCategoryMappingProfile : AutoMapper.Profile
    {
        public ProductCategoryMappingProfile()
        {
            CreateMap<ProductCategory, ProductCategoryVM>().ReverseMap();
        }
    }
}

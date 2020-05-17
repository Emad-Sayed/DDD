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
            CreateMap<Product, AlgoliaProductVM>()
                //.ForMember(pro => pro.ObjectID, op => op.MapFrom(y => y.Id.ToString()))
                //.ForMember(pro => pro.BrandId, op => op.MapFrom(y => y.BrandId.ToString()))
                //.ForMember(pro => pro.ProductCategoryId, op => op.MapFrom(y => y.ProductCategoryId.ToString()))
                .ReverseMap();
            CreateMap<Brand, BrandVM>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryVM>().ReverseMap();
        }
    }
}

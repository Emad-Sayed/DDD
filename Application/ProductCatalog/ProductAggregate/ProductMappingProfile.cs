using Application.ProductCatalog.ProductAggregate.ViewModels;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.ProductAggregate
{
    public class ProductMappingProfile : AutoMapper.Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, AlgoliaProductVM>()
                .ForMember(pro => pro.ObjectID, op => op.MapFrom(y => y.Id.ToString()))
                .ForMember(pro => pro.Category, op => op.MapFrom(y => y.ProductCategory.Name))
                .ForMember(pro => pro.Brand, op => op.MapFrom(y => y.Brand.Name))
                .ReverseMap();

            CreateMap<Unit, AlgoliaUnitVM>()
                .ForMember(pro => pro.Unit, op => op.MapFrom(y => y.Name))
                .ReverseMap();

            CreateMap<Product, ProductVM>().ReverseMap();

            CreateMap<Unit, UnitVM>().ReverseMap();
        }
    }
}

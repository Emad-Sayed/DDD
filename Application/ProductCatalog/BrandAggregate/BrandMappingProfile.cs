using Application.ProductCatalog.BrandAggregate.ViewModels;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProductCatalog.BrandAggregate
{
    public class BrandMappingProfile : AutoMapper.Profile
    {
        public BrandMappingProfile()
        {
            CreateMap<Brand, BrandVM>().ReverseMap();
        }
    }
}

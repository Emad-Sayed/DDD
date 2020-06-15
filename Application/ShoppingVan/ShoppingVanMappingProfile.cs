using Application.ShoppingVan.ViewModels;
using Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ShoppingVan
{
    public class ShoppingVanMappingProfile : AutoMapper.Profile
    {
        public ShoppingVanMappingProfile()
        {
            CreateMap<Van, ShoppingVanVM>().ReverseMap();
            CreateMap<VanItem, ShoppingVanItemVM>().ReverseMap();
        }
    }
}

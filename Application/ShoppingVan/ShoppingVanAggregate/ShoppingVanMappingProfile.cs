using Application.ShoppingVanBoundedContext.ShoppingVanAggregate.ViewModels;
using Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ShoppingVanBoundedContext.ShoppingVanAggregate
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

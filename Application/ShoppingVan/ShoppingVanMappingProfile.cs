using Application.ShoppingVan.ViewModels;
using Domain.ShoppingVan.AggregatesModel.ShoppingVanAggregate;
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
            CreateMap<Van, ShoppingVanVM>()
                .ForMember(x => x.TotalPrice, y => y.MapFrom(z => z.TotalPrice + (z.TotalPrice * 0.14f)))
                .ReverseMap();
            CreateMap<VanItem, ShoppingVanItemVM>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.ProductName))
                .ForMember(x => x.ImgUrl, y => y.MapFrom(z => z.PhotoUrl))
             .ReverseMap();
            CreateMap<Unit, UnitVM>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.UnitId))
                .ForMember(x => x.ConsumerPrice, y => y.MapFrom(z => z.SellingPrice))
                .ReverseMap();
        }
    }
}

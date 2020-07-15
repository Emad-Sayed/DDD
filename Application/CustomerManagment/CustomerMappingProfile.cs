using Application.CustomerManagment.ViewModels;
using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CustomerManagment
{
    public class CustomerMappingProfile : AutoMapper.Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomerVM>()
                .ForMember(x => x.City, op => op.MapFrom(y => y.Address.City))
                .ForMember(x => x.Area, op => op.MapFrom(y => y.Address.Area))
                .ReverseMap();
            CreateMap<City, CityVM>().ReverseMap();
            CreateMap<Area, AreaVM>().ReverseMap();
        }
    }
}

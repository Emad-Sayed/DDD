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
            CreateMap<Customer, CustomerVM>().ReverseMap();
            CreateMap<City, CityVM>().ReverseMap();
            CreateMap<Region, RegionVM>().ReverseMap();
        }
    }
}

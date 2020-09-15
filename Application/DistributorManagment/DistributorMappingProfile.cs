using Application.DistributorManagment.ViewModels;
using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DistributorManagment
{
    public class DistributorMappingProfile : AutoMapper.Profile
    {
        public DistributorMappingProfile()
        {
            CreateMap<Distributor, DistributorVM>().ReverseMap();
            CreateMap<City, CityVM>().ReverseMap();
            CreateMap<Area, AreaVM>().ReverseMap();
            CreateMap<DistributorUser, DistributorUserVM>().ReverseMap();
        }
    }
}

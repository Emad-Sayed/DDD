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
            CreateMap<Distributor, DistributorVM>();
            CreateMap<City, CityVM>().ReverseMap();
            CreateMap<Region, RegionVM>().ReverseMap();
            CreateMap<DistributorUser, DistributorUserVM>().ReverseMap();
        }
    }
}

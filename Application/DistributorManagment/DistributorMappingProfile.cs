using Application.DistributorManagment.ViewModels;
using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.DistributorManagment
{
    public class DistributorMappingProfile : AutoMapper.Profile
    {
        public DistributorMappingProfile()
        {
            CreateMap<Distributor, DistributorVM>()
                .ForMember(x => x.Cities, op => op.MapFrom(y => y.DistributorAreas.Select(x => x.Area).ToList().Select(x => x.City).Distinct().ToList())).ReverseMap();
            CreateMap<City, CityVM>().ReverseMap();
            CreateMap<Area, AreaVM>().ReverseMap();
            CreateMap<DistributorUser, DistributorUserVM>().ReverseMap();
        }
    }
}

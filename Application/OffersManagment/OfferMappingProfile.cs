using Application.OffersManagment.ViewModels;
using Domain.OffersManagment.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OffersManagment
{
    public class OfferMappingProfile : AutoMapper.Profile
    {
        public OfferMappingProfile()
        {
            CreateMap<Offer, OfferVM>().ReverseMap();
            CreateMap<Product, ProductVM>().ReverseMap();
        }
    }
}

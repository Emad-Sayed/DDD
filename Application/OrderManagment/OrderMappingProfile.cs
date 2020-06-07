using Application.OrderManagment.ViewModels;
using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.OrderManagment
{
    public class OrderMappingProfile : AutoMapper.Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderVM>().ReverseMap();
            CreateMap<OrderItem, OrderItemVM>().ReverseMap();
        }
    }
}

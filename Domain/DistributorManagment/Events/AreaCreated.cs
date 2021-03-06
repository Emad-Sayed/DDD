using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.Events
{
    public class AreaCreated : INotification
    {
        public City City { get; }
        public Area Area { get; }

        public AreaCreated(City city, Area area)
        {
            City = city;
            Area = area;
        }
    }
}

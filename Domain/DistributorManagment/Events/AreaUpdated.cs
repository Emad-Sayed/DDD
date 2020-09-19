using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.Events
{
    public class AreaUpdated : INotification
    {
        public City City { get; }
        public Area Area { get; }

        public AreaUpdated(City city, Area area)
        {
            City = city;
            Area = area;
        }
    }
}

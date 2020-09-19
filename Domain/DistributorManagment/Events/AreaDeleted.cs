using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.Events
{
    public class AreaDeleted : INotification
    {
        public City City { get; }
        public Area Area { get; }

        public AreaDeleted(City city,Area area)
        {
            City = city;
            Area = area;
        }
    }
}

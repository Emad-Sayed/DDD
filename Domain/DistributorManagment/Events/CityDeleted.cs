using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.Events
{
    public class CityDeleted : INotification
    {
        public City City { get; }

        public CityDeleted(City city)
        {
            City = city;
        }
    }
}

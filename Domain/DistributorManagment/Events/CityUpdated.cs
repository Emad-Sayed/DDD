using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.Events
{
    public class CityUpdated : INotification
    {
        public City City { get; }

        public CityUpdated(City city)
        {
            City = city;
        }
    }
}

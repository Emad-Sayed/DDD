using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.Events
{
    public class CityCreated : INotification
    {
        public City City { get; }

        public CityCreated(City city)
        {
            City = city;
        }
    }
}

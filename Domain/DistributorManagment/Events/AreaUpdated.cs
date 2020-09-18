using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.Events
{
    public class AreaUpdated : INotification
    {
        public Area Area { get; }

        public AreaUpdated(Area area)
        {
            Area = area;
        }
    }
}

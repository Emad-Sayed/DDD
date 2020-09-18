using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.Events
{
    public class AreaDeleted : INotification
    {
        public Area Area { get; }

        public AreaDeleted(Area area)
        {
            Area = area;
        }
    }
}

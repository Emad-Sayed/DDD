using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.Events
{
    public class DistributorUserUpdated : INotification
    {
        public DistributorUser DistributorUser { get; }

        public DistributorUserUpdated(DistributorUser distributorUser)
        {
            DistributorUser = distributorUser;
        }
    }
}

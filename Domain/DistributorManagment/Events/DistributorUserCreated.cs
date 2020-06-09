using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.Events
{
    public class DistributorUserCreated : INotification
    {
        public DistributorUser DistributorUser { get; }

        public DistributorUserCreated(DistributorUser distributorUser)
        {
            DistributorUser = distributorUser;
        }
    }
}

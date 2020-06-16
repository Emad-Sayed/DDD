using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.Events
{
    public class DistributorUserDeleted : INotification
    {
        public DistributorUser DistributorUser { get; }

        public DistributorUserDeleted(DistributorUser distributorUser)
        {
            DistributorUser = distributorUser;
        }
    }
}

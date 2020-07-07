using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.Events
{
    public class DistributorUserEmailConfirmed : INotification
    {
        public DistributorUser DistributorUser { get; }

        public DistributorUserEmailConfirmed(DistributorUser distributorUser)
        {
            DistributorUser = distributorUser;
        }
    }
}

using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.Events
{
    public class DistributorUpdated : INotification
    {
        public Distributor Distributor { get; }

        public DistributorUpdated(Distributor distributor)
        {
            Distributor = distributor;
        }
    }
}

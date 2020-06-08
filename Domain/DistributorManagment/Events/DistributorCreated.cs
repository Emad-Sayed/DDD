using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.Events
{
    public class DistributorCreated : INotification
    {
        public Distributor Distributor { get; }

        public DistributorCreated(Distributor distributor)
        {
            Distributor = distributor;
        }
    }
}

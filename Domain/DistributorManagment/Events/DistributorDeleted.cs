using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.Events
{
    public class DistributorDeleted : INotification
    {
        public Distributor Distributor { get; }

        public DistributorDeleted(Distributor distributor)
        {
            Distributor = distributor;
        }
    }
}

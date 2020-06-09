using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.AggregatesModel.DistributorAggregate
{
    public class DistributorUser: EntityBase
    {
        public string AccountId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public string DistributorId { get; set; }
        public Distributor Distributor { get; set; }

    }
}

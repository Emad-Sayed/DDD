using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.AggregatesModel.DistributorAggregate
{
    public class DistributorArea : EntityBase
    {
        public new string Id { get; set; }
        public string AreaId { get; set; }
        public Area Area { get; set; }

        public Guid DistributorId { get; set; }
        public Distributor Distributor { get; set; }
    }
}

using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.CustomerManagment.AggregatesModel.CustomerAggregate
{
    public class Region: EntityBase
    {
        public string Name { get; private set; }

        public string CityId { get; private set; }
        public City City { get; private set; }
    }
}

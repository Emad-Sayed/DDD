using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.CustomerManagment.AggregatesModel.CustomerAggregate
{
    public class City : EntityBase
    {
        public string Name { get; private set; }
        public ICollection<Area> Areas { get; private set; }
    }
}

using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.AggregatesModel.DistributorAggregate
{
    public class City : EntityBase
    {
        public new string Id { get; protected set; }
        public string Name { get; private set; }
        public ICollection<Area> Areas { get; private set; }

        private City() { }
        public City(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

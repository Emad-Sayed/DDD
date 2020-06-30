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

        private City() { }

        public City(string name, Guid id = default)
        {
            Name = name;
            Id = id == default ? Guid.NewGuid() : id;
        }
    }
}

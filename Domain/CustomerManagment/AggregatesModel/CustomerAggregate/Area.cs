using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.CustomerManagment.AggregatesModel.CustomerAggregate
{
    public class Area: EntityBase
    {
        public string Name { get; private set; }

        public string CityId { get; private set; }
        public City City { get; private set; }

        private Area() { }

        public Area(string name,string cityId, Guid id = default)
        {
            Name = name;
            CityId = cityId;

            Id = id == default ? Guid.NewGuid() : id;
        }
    }
}

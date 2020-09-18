using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.CustomerManagment.AggregatesModel.CustomerAggregate
{
    public class Area : EntityBase
    {
        public new string Id { get; private set; }
        public string Name { get; private set; }

        public string CityId { get; private set; }
        public City City { get; private set; }

        public ICollection<Customer> Customers { get; private set; }

        private Area() { }

        public Area(string name, string cityId, string id)
        {
            Name = name;
            CityId = cityId;

            Id = id;
        }

        public void UpdateArea(string name)
        {
            Name = name;
        }
    }
}

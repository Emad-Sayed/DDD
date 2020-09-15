using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.AggregatesModel.DistributorAggregate
{
    public class Area : EntityBase
    {
        public new string Id { get; protected set; }
        public string Name { get; private set; }

        public string CityId { get; private set; }
        public City City { get; private set; }
        public ICollection<DistributorArea> DistributorAreas { get; private set; }

        private Area() { }

        public Area(string name, string cityId, string id)
        {
            Name = name;
            CityId = cityId;

            Id = id;
        }
    }
}

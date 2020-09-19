using Domain.Base.Entity;
using Domain.DistributorManagment.Events;
using Domain.DistributorManagment.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

            // Add the CityCreated to the domain events collection 
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            // Register Placing order event
            AddDomainEvent(new CityCreated(this));
        }

        public void UpdateCity(string name)
        {
            Name = name;

            AddDomainEvent(new CityUpdated(this));
        }

        public void DeleteCity()
        {
            AddDomainEvent(new CityDeleted(this));
        }

        public Area AddArea(string name)
        {
            var area = Areas.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
            if (area != null) throw new AreaAlreadyExistException(name);

            area = new Area(name, Id, Guid.NewGuid().ToString());

            Areas.Add(area);

            AddDomainEvent(new AreaCreated(this, area));

            return area;
        }

        public void UpdateArea(string id, string name)
        {
            var area = Areas.FirstOrDefault(x => x.Id == id);
            if (area == null) throw new AreaNotFoundException(id);

            if (area.Name.ToLower() == name.ToLower()) throw new AreaAlreadyExistException(name);

            area.UpdateArea(name);

            AddDomainEvent(new AreaUpdated(this, area));
        }

        public void DeleteArea(string id)
        {
            var area = Areas.FirstOrDefault(x => x.Id == id);
            if (area == null) throw new AreaNotFoundException(id);

            Areas.Remove(area);

            AddDomainEvent(new AreaDeleted(this, area));
        }


    }
}

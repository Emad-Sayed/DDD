using Domain.Base.Entity;
using Domain.CustomerManagment.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.CustomerManagment.AggregatesModel.CustomerAggregate
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

        public void UpdateCity(string name)
        {
            Name = name;
        }


        public Area AddArea(string name)
        {
            var area = Areas.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
            if (area != null) throw new AreaAlreadyExistException(name);

            area = new Area(name, Id, Guid.NewGuid().ToString());

            Areas.Add(area);

            return area;
        }

        public void UpdateArea(string id, string name)
        {
            var area = Areas.FirstOrDefault(x => x.Id == id);
            if (area == null) throw new AreaNotFoundException(id);

            if (area.Name.ToLower() == name.ToLower()) throw new AreaAlreadyExistException(name);

            area.UpdateArea(name);
        }

        public void DeleteCity()
        {

        }

        public void DeleteArea(string id)
        {
            var area = Areas.FirstOrDefault(x => x.Id == id);
            if (area == null) throw new AreaNotFoundException(id);

            Areas.Remove(area);
        }

    }
}

using Domain.Base.Entity;
using Domain.ShoppingVan.AggregatesModel.ShoppingVanAggregate;
using Domain.ShoppingVan.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate
{
    public class VanItem : AuditableEntity
    {
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string PhotoUrl { get; private set; }

        public string VanId { get; private set; }
        public Van Van { get; private set; }

        public ICollection<Unit> Units { get; private set; }

        private VanItem() { }

        public VanItem(string vanId, string productId, string productName, string photoUrl, List<Unit> units, Guid id = default)
        {
            ProductId = productId;
            ProductName = productName;
            VanId = vanId;
            PhotoUrl = photoUrl;

            //Id = id == default ? Guid.NewGuid() : id;

            Units = units;
        }


        public Unit DecreaseUnit(string unitId)
        {
            var unit = Units.FirstOrDefault(x => x.UnitId == unitId);
            if (unit == null) throw new UnitNotFoundException(unitId);

            unit.DecreaseUnit();
            return unit;
        }

        public Unit IncreaseUnit(string unitId)
        {
            var unit = Units.FirstOrDefault(x => x.UnitId == unitId);
            if (unit == null) throw new UnitNotFoundException(unitId);
            unit.IncreaseUnit();
            return unit;
        }


    }
}

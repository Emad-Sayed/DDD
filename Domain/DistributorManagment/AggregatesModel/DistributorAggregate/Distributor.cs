using Domain.Base.Entity;
using Domain.Common.Interfaces;
using Domain.DistributorManagment.Events;
using Domain.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.AggregatesModel.DistributorAggregate
{
    public class Distributor : EntityBase, IAggregateRoot
    {
        public string Name { get; private set; }
        public Address Address { get; private set; }

        public ICollection<DistributorUser> DistributorUsers { get; private set; }


        private Distributor()
        {
        }

        public Distributor(string name, string city, string region)
        {
            Name = name;
            Address = new Address(region, city);

            // Add the OrderStarterDomainEvent to the domain events collection 
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            // Register Placing order event
            AddDomainEvent(new DistributorCreated(this));
        }

    }
}

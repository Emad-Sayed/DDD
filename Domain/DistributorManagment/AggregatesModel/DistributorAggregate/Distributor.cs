using Domain.Base.Entity;
using Domain.Common.Interfaces;
using Domain.DistributorManagment.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.AggregatesModel.DistributorAggregate
{
    public class Distributor : EntityBase, IAggregateRoot
    {
        public string AccountId { get; private set; }
        public string LocationOnMap { get; private set; }
        public string FullName { get; private set; }

        private Distributor()
        {
        }

        public Distributor(string accountId, string fullName, string locationOnMap)
        {
            AccountId = accountId;
            FullName = fullName;
            LocationOnMap = locationOnMap;

            // Add the OrderStarterDomainEvent to the domain events collection 
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            // Register Placing order event
            AddDomainEvent(new DistributorCreated(this));
        }

    }
}

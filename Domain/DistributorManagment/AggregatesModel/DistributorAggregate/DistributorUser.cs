using Domain.Base.Entity;
using Domain.DistributorManagment.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DistributorManagment.AggregatesModel.DistributorAggregate
{
    public class DistributorUser : EntityBase
    {
        public string AccountId { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public bool EmailConfirmed { get; private set; }

        public string DistributorId { get; private set; }
        public Distributor Distributor { get; private set; }


        private DistributorUser() { }

        public DistributorUser(string distributorId, string accountId, string fullName, string email)
        {
            DistributorId = distributorId;
            AccountId = accountId;
            FullName = fullName;
            Email = email;

            // Add the DistributorUserCreated to the domain events collection 
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            // Register Placing order event
            AddDomainEvent(new DistributorUserCreated(this));
        }

        public void Update(string fullName)
        {
            FullName = fullName;
        }

        public void ConfirmEmail()
        {
            EmailConfirmed = true;
        }
    }
}

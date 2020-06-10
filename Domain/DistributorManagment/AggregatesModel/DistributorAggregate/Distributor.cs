using Domain.Base.Entity;
using Domain.Common.Exceptions;
using Domain.Common.Interfaces;
using Domain.DistributorManagment.Events;
using Domain.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
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
            DistributorUsers = new List<DistributorUser>();

            // Add the DistributorCreated to the domain events collection 
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            // Register Placing order event
            AddDomainEvent(new DistributorCreated(this));
        }

        public void CreateUser(string accountId, string fullName, string email)
        {
            var distributorUserToCreate = new DistributorUser(Id.ToString(), accountId, fullName, email);
            DistributorUsers.Add(distributorUserToCreate);
        }

        // update distributor
        public void UpdateDistributor(string name, string city, string region)
        {
            Name = name;
            Address = new Address(region, city);

            // rais distributor updated event
            AddDomainEvent(new DistributorUpdated(this));
        }

        // Remove Distributor User from distributor
        public void DeleteDistributorUser(string distributorUserId)
        {
            var distributorUser = DistributorUsers.FirstOrDefault(x => x.Id.ToString() == distributorUserId);
            if (distributorUser == null)
                throw new NotFoundException(nameof(DistributorUsers));

            DistributorUsers.Remove(distributorUser);

            // rais Distributor updated event
            AddDomainEvent(new DistributorUpdated(this));
        }

        // update  Distributor User
        public void UpdateDistributorUser(string distributorUserId, string fullName)
        {
            var distributorUserToUpdate = DistributorUsers.FirstOrDefault(x => x.Id.ToString() == distributorUserId);
            if (distributorUserToUpdate == null)
                throw new NotFoundException(nameof(DistributorUsers));

            distributorUserToUpdate.Update(fullName);

            // rais product updated event
            AddDomainEvent(new DistributorUpdated(this));
        }
    }
}

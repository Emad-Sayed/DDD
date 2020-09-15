using Domain.Base.Entity;
using Domain.Common.Exceptions;
using Domain.Common.Interfaces;
using Domain.DistributorManagment.Events;
using Domain.DistributorManagment.Exceptions;
using Domain.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Domain.DistributorManagment.AggregatesModel.DistributorAggregate
{
    public class Distributor : AuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; }

        public ICollection<DistributorUser> DistributorUsers { get; private set; }
        public ICollection<DistributorArea> DistributorAreas { get; private set; }


        private Distributor()
        {
        }

        public Distributor(string name)
        {
            Name = name;
            DistributorUsers = new List<DistributorUser>();
            DistributorAreas = new List<DistributorArea>();

            // Add the DistributorCreated to the domain events collection 
            // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
            // Register Placing order event
            AddDomainEvent(new DistributorCreated(this));
        }

        public void CreateUser(string accountId, string fullName, string email)
        {
            var distributorUserToCreate = new DistributorUser(Id.ToString(), accountId, fullName, email);
            DistributorUsers.Add(distributorUserToCreate);
            AddDomainEvent(new DistributorUserCreated(distributorUserToCreate));
        }

        public void AddArea(Area area)
        {
            var distributorArea = new DistributorArea { Area = area, Distributor = this };
            DistributorAreas.Add(distributorArea);
        }


        // update distributor
        public void UpdateDistributor(string name)
        {
            Name = name;

            // rais distributor updated event
            AddDomainEvent(new DistributorUpdated(this));
        }

        // Remove Distributor User from distributor
        public void DeleteDistributorUser(string distributorUserId)
        {
            var distributorUser = DistributorUsers.FirstOrDefault(x => x.Id.ToString() == distributorUserId);
            if (distributorUser == null) throw new DistributorUserNotFoundException(distributorUserId);


            DistributorUsers.Remove(distributorUser);

            // rais Distributor updated event
            AddDomainEvent(new DistributorUserDeleted(distributorUser));
        }

        // update  Distributor User
        public void UpdateDistributorUser(string distributorUserId, string fullName)
        {
            var distributorUserToUpdate = DistributorUsers.FirstOrDefault(x => x.Id.ToString() == distributorUserId);
            if (distributorUserToUpdate == null) throw new DistributorUserNotFoundException(distributorUserId);


            distributorUserToUpdate.Update(fullName);

            // rais product updated event
            AddDomainEvent(new DistributorUserUpdated(distributorUserToUpdate));
        }

        // Confirm  Distributor User Email
        public void ConfirmDistributorUserEmail(string distributorUserId)
        {
            var distributorUserToConfirmEmail = DistributorUsers.FirstOrDefault(x => x.Id.ToString() == distributorUserId);
            if (distributorUserToConfirmEmail == null) throw new DistributorUserNotFoundException(distributorUserId);


            distributorUserToConfirmEmail.ConfirmEmail();

            // rais product updated event
            AddDomainEvent(new DistributorUserEmailConfirmed(distributorUserToConfirmEmail));
        }
    }
}

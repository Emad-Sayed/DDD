using Domain.Base.Entity;
using Domain.Common.Interfaces;
using Domain.CustomerManagment.Events;
using Domain.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.CustomerManagment.AggregatesModel.CustomerAggregate
{
    public class Customer : AuditableEntity, IAggregateRoot
    {
        public string AccountId { get; private set; }
        public string FullName { get; private set; }
        public string ShopName { get; private set; }
        public string ShopAddress { get; private set; }
        public string LocationOnMap { get; private set; }
        public Address Address { get; private set; }
        public bool IsDeleted { get; private set; }

        private Customer() { }

        public Customer(string accountId, string city, string area, string fullName, string shopName, string shopAddress, string locationOnMap, Guid id = default)
        {
            AccountId = accountId;
            FullName = fullName;
            ShopName = shopName;
            ShopAddress = shopAddress;
            LocationOnMap = locationOnMap;
            Address = new Address(area, city);

            Id = id == default ? Guid.NewGuid() : id;
            AddDomainEvent(new CustomerCreated(this));
        }

        public void UpdateCustomer(string city, string area, string fullName, string shopName, string shopAddress, string locationOnMap)
        {
            FullName = fullName;
            ShopName = shopName;
            ShopAddress = shopAddress;
            LocationOnMap = locationOnMap;
            Address = new Address(area, city);
            AddDomainEvent(new CustomerUpdated(this));
        }

        // delete customer
        public void Delete()
        {
            IsDeleted = true;
            // rais product deleted event
            AddDomainEvent(new CustomerDeleted(this));
        }
    }
}

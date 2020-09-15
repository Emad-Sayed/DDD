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
        public string CustomerCode { get; private set; }
        public string FullName { get; private set; }
        public string ShopName { get; private set; }
        public string ShopAddress { get; private set; }
        public string LocationOnMap { get; private set; }
        public bool IsActive { get; private set; }

        public string AreaId { get; private set; }
        public Area Area { get; private set; }

        private Customer() { }

        public Customer(string accountId, string fullName, string shopName, string shopAddress, string locationOnMap, Guid id = default)
        {
            AccountId = accountId;
            FullName = fullName;
            ShopName = shopName;
            ShopAddress = shopAddress;
            LocationOnMap = locationOnMap;

            Id = id == default ? Guid.NewGuid() : id;
            AddDomainEvent(new CustomerCreated(this));
        }

        public void AddArea(Area area)
        {
            Area = area;
        }

        public void UpdateCustomer(string fullName, string shopName, string shopAddress, string locationOnMap)
        {
            FullName = fullName;
            ShopName = shopName;
            ShopAddress = shopAddress;
            LocationOnMap = locationOnMap;
            AddDomainEvent(new CustomerUpdated(this));
        }

        // ActiveAndDeactiveCustomer customer
        public void ActiveAndDeactiveCustomer()
        {
            IsActive = !IsActive;
            // rais product deleted event
            AddDomainEvent(new CustomerActivedOrDeactived(this));
        }
    }
}

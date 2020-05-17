using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.CustomerManagment.Customer.DomainModels
{
    public class Customer : EntityBase
    {
        public string AccountId { get; private set; }
        public string ShopName { get; private set; }
        public string ShopAddress { get; private set; }
        public string LocationOnMap { get; private set; }

        private Customer()
        {

        }
        public Customer(string accountId,string shopName,string shopAddress, string locationOnMap, Guid id = default)
        {
            AccountId = accountId;
            ShopName = shopName;
            ShopAddress = shopAddress;
            LocationOnMap = locationOnMap;

            Id = id == default ? Guid.NewGuid() : id;
        }
    }
}

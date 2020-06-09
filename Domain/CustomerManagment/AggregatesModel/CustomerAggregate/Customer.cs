﻿using Domain.Base.Entity;
using Domain.Common.Interfaces;
using Domain.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.CustomerManagment.AggregatesModel.CustomerAggregate
{
    public class Customer : EntityBase, IAggregateRoot
    {
        public string AccountId { get; private set; }
        public string ShopName { get; private set; }
        public string ShopAddress { get; private set; }
        public string LocationOnMap { get; private set; }
        public Address Address { get; private set; }

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
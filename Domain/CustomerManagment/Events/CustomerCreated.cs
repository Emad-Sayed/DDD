using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.CustomerManagment.Events
{
    public class CustomerCreated : INotification
    {
        public Customer Customer { get; }

        public CustomerCreated(Customer customer)
        {
            Customer = customer;
        }
    }
}

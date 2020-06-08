using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.CustomerManagment.Events
{
    public class CustomerUpdated : INotification
    {
        public Customer Customer { get; }

        public CustomerUpdated(Customer customer)
        {
            Customer = customer;
        }
    }
}

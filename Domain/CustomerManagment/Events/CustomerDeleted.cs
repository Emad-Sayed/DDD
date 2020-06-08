using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.CustomerManagment.Events
{
    public class CustomerDeleted : INotification
    {
        public Customer Customer { get; }

        public CustomerDeleted(Customer customer)
        {
            Customer = customer;
        }
    }
}

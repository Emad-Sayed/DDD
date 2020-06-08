using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CustomerManagment.Commands.CreateCustomer
{
    public class CustomerCreated : INotification
    {
        public string CustomerId { get; set; }

        public class CustomerCreatedHandler : INotificationHandler<CustomerCreated>
        {
            public CustomerCreatedHandler()
            {
            }

            public Task Handle(CustomerCreated notification, CancellationToken cancellationToken)
            {
                return Task.CompletedTask;
            }
        }
    }
}

using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using MediatR;

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

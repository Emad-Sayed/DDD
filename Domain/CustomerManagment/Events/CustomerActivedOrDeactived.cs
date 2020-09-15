using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using MediatR;

namespace Domain.CustomerManagment.Events
{
    public class CustomerActivedOrDeactived : INotification
    {
        public Customer Customer { get; }

        public CustomerActivedOrDeactived(Customer customer)
        {
            Customer = customer;
        }
    }
}

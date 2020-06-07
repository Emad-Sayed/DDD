using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using MediatR;
using System.Globalization;

namespace Domain.OrderManagment.Events
{
    /// <summary>
    /// Event used when an order is created
    /// </summary>
    public class OrderPlaced : INotification
    {
        public string CustomerId { get; }
        public string Address { get; }

        public Order Order { get; }

        public OrderPlaced(Order order, string customerId, string address)
        {
            Order = order;
            CustomerId = customerId;
            Address = address;
        }
    }
}

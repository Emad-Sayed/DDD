namespace Domain.OrderManagment.AggregatesModel.OrderAggregate
{
    public enum OrderStatus
    {
        Placed,
        Confirmed,
        Shipped,
        Delivered,
        Cancelled
    }
}

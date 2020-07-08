using Domain.Base.Entity;

namespace Domain.OrderManagment.AggregatesModel.OrderAggregate
{
    public class OrderItem
        : AuditableEntity
    {
        // DDD Patterns comment
        // Using private fields, allowed since EF Core 1.1, is a much better encapsulation
        // aligned with DDD Aggregates and Domain Entities (Instead of properties and property collections)

        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string PhotoUrl { get; private set; }
        public float UnitPrice { get; private set; }
        public string UnitId { get; private set; }
        public string UnitName { get; private set; }
        public int UnitCount { get; private set; }

        public string OrderId { get; set; }
        public Order Order { get; set; }

        protected OrderItem() { }

        public OrderItem(string orderId, string productId, string productName, float unitPrice, string photoUrl, string unitId, string unitName, int unitCount = 1)
        {
            OrderId = orderId;
            ProductId = productId;

            ProductName = productName;
            UnitPrice = unitPrice;
            UnitCount = unitCount;
            PhotoUrl = photoUrl;
            UnitId = unitId;
            UnitName = unitName;
        }

        public void Update(string unitId, string unitName,float unitPrice, int unitCount = 1)
        {
            UnitCount = unitCount;
            UnitId = unitId;
            UnitPrice = unitPrice;
            UnitName = unitName;
        }
    }

}

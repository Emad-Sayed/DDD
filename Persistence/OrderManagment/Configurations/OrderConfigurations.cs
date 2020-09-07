using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.OrderManagment.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.OrderNumber).HasDefaultValueSql("NEXT VALUE FOR shared.OrderNumbers");
            builder.Ignore(b => b.DomainEvents);
        }
    }
}

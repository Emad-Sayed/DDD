using Domain.Order.AggregatesModel.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Order.Configurations
{
    public class BrandConfigurations : IEntityTypeConfiguration<Domain.Order.AggregatesModel.OrderAggregate.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Order.AggregatesModel.OrderAggregate.Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(b => b.DomainEvents);

            //Address value object persisted as owned entity type supported since EF Core 2.0
            builder.OwnsOne(o => o.Address, a =>
                {
                    a.WithOwner();
                });
        }
    }
}

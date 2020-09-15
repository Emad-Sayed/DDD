using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.CustomerManagment.Configurations
{
    public class AreaConfigurations : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.CityId).HasColumnName("CityId");
            builder.HasOne(x => x.City);
            builder.HasMany(x => x.Customers);
        }
    }
}

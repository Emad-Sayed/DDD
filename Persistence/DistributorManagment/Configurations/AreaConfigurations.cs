using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.DistributorManagment.Configurations
{
    public class AreaConfigurations : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.Property(x => x.CityId).HasColumnName("CityId");
            builder.HasOne(x => x.City);
        }
    }
}

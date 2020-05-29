using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.ShoppingVan.Configurations
{
    public class ShoppingVanConfigurations : IEntityTypeConfiguration<Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate.Van>
    {
        public void Configure(EntityTypeBuilder<Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate.Van> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}

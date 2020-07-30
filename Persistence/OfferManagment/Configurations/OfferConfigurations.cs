using Domain.OffersManagment.AggregatesModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.OfferManagment.Configurations
{
    public class OfferConfigurations : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(b => b.DomainEvents);
        }
    }
}

using Application.ProductCatalog;
using Domain.ProductCatalog.Product.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.ProductCatalog
{

    public class ProductCatalogContext : DbContext, IProductCatalogContext
    {
        public ProductCatalogContext(DbContextOptions<ProductCatalogContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductCatalogContext).Assembly, type => type.FullName.Contains("ProductCatalog"));
        }
    }
}

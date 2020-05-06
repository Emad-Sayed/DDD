using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.ProductCatalog.Products.DomainModels;

namespace Application.ProductCatalog
{
    public interface IProductCatalogContext
    {
        public DbSet<Product> Products { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());

        int SaveChanges();
    }
}

using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Microsoft.EntityFrameworkCore;
using Persistence.ProductCatalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ProductCatalog
{
    public class BrandRepository
        : IBrandRepository
    {
        private readonly ProductCatalogContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public BrandRepository(ProductCatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Brand Add(Brand brand)
        {
            return _context.Brands
                   .Add(brand)
                   .Entity;
        }

        public Brand Update(Brand brand)
        {
            throw new NotImplementedException();
        }

        public Task<Brand> FindAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Brand> FindByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<(int, List<Brand>)> GetAllBrands()
        {
            var query = _context.Brands.AsQueryable();

            var totalBrands = await query.CountAsync();

            var brandsFromRepo = await query.ToListAsync();

            return (totalBrands, brandsFromRepo);
        }
    }
}

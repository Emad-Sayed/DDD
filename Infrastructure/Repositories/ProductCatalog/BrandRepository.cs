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
            return _context.Brands.Add(brand).Entity;
        }

        public void AddRange(List<Brand> brands)
        {
            _context.Brands.AddRange(brands);
        }

        public void Update(Brand brand)
        {
            _context.Entry(brand).State = EntityState.Modified;
        }

        public void Delete(Brand brand)
        {
            _context.Brands.Remove(brand);
        }

        public async Task<Brand> FindByIdAsync(string brandId)
        {
            return await _context.Brands.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id.ToString() == brandId);
        }

        public async Task<(int, List<Brand>)> GetAllBrands()
        {
            var query = _context.Brands.AsQueryable();

            query = query.OrderByDescending(x => x.Created);

            var totalBrands = await query.CountAsync();

            var brandsFromRepo = await query.ToListAsync();

            return (totalBrands, brandsFromRepo);
        }

        public async Task<(int, List<Brand>)> GetBrands(int pageNumber, int pageSize, string keyWord)
        {
            var query = _context.Brands
                .AsQueryable();

            // fillter by keyword
            if (!string.IsNullOrEmpty(keyWord))
            {
                query = query.Where(x =>
                x.Id.ToString().Contains(keyWord) ||
                x.Name.Contains(keyWord)
                );
            }

            query = query.OrderByDescending(x => x.Created);

            // apply pagination to products
            var brands = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = query.Count();

            return (count, brands);
        }

        public void DeleteAll()
        {
            _context.Brands.RemoveRange(_context.Brands);
        }
    }
}

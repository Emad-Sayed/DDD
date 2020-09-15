using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Persistence.ProductCatalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ProductCatalog
{
    public class ProductCategoryRepository
        : IProductCategoryRepository
    {
        private readonly ProductCatalogContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public ProductCategoryRepository(ProductCatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ProductCategory Add(ProductCategory productCategory)
        {
            return _context.ProductCategories.Add(productCategory).Entity;
        }

        public void AddRange(List<ProductCategory> productCategories)
        {
            _context.ProductCategories.AddRange(productCategories);
        }
        public void Update(ProductCategory productCategory)
        {
            _context.Entry(productCategory).State = EntityState.Modified;
        }

        public void Delete(ProductCategory productCategory)
        {
            _context.ProductCategories.Remove(productCategory);
        }

        public async Task<ProductCategory> FindByIdAsync(string productCategoryId)
        {
            return await _context.ProductCategories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id.ToString() == productCategoryId);
        }

        public async Task<(int, List<ProductCategory>)> GetAllProductCategorys()
        {
            var query = _context.ProductCategories.AsQueryable();

            query = query.OrderByDescending(x => x.Created);

            var totalProductCategorys = await query.CountAsync();

            var productCategorysFromRepo = await query.ToListAsync();

            return (totalProductCategorys, productCategorysFromRepo);
        }

        public async Task<(int, List<ProductCategory>)> GetProductCategorys(int pageNumber, int pageSize, string keyWord)
        {
            var query = _context.ProductCategories
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
            var productCategories = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = query.Count();

            return (count, productCategories);
        }

        public void DeleteAll()
        {
            _context.ProductCategories.RemoveRange(_context.ProductCategories);
        }

        public ProductCategory AddProductCategoryIfNotExist(string productCategoryName)
        {
            var productCategory = _context.ProductCategories.FirstOrDefault(x => x.Name.ToLower() == productCategoryName.ToLower());
            if (productCategory == null)
            {
                productCategory = new ProductCategory(productCategoryName);
                _context.ProductCategories.Add(productCategory);
            }

            return productCategory;
        }
    }
}

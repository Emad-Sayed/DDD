﻿using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Persistence.ProductCatalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ProductCatalog
{
    public class ProductRepository
        : IProductRepository
    {
        private readonly ProductCatalogContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public ProductRepository(ProductCatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Product Add(Product product)
        {
            return _context.Products
                   .Add(product)
                   .Entity;
        }

        public void Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }

        public async Task<(int, List<Product>)> GetAllAsync(int pageNumber, int pageSize, string keyWord)
        {
            var query = _context.Products
                .Where(x => x.IsDeleted == false)
                .Include(x => x.ProductCategory)
                .Include(x => x.Brand)
                .AsQueryable();

            // fillter by keyword
            if (!string.IsNullOrEmpty(keyWord))
            {
                query = query.Where(x =>
                x.Barcode.Contains(keyWord) ||
                x.Id.ToString().Contains(keyWord) ||
                x.Name.Contains(keyWord) ||
                x.Brand.Name.Contains(keyWord) ||
                x.ProductCategory.Name.Contains(keyWord)
                );
            }

            // apply pagination to products
            var products = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = query.Count();

            return (count, products);
        }

        public async Task<Product> FindByIdAsync(string id)
        {
            return await _context.Products
                .Where(x => x.IsDeleted == false)
                   .Include(x => x.Brand)
                   .Include(x => x.Units)
                   .Include(x => x.ProductCategory)
                   .FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<List<Unit>> GetProductsUnits(List<string> productsIds)
        {
            var units = await _context.Units.Where(x => x.IsDeleted == false).Where(x => productsIds.Contains(x.ProductId.ToString())).ToListAsync();
            return units;
        }

        public async Task<Brand> GetBrandById(string brandId)
        {
            return await _context.Brands
                  .FirstOrDefaultAsync(x => x.Id.ToString() == brandId);
        }

        public async Task<ProductCategory> GetProductCategoryById(string productCategoryId)
        {
            return await _context.ProductCategories
                  .FirstOrDefaultAsync(x => x.Id.ToString() == productCategoryId);
        }
    }
}

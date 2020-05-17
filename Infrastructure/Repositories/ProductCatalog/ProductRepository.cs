﻿using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Microsoft.EntityFrameworkCore;
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

        public Product Update(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<Product> FindAsync(string id)
        {
            return _context.Products
                .Include(x => x.Brand)
                .Include(x => x.ProductCategory)
                .FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        public Task<Product> FindByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}

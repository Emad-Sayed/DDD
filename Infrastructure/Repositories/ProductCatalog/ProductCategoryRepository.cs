using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using Persistence.ProductCatalog;
using System;
using System.Collections.Generic;
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
            return _context.ProductCategories
                   .Add(productCategory)
                   .Entity;
        }

        public ProductCategory Update(ProductCategory productCategory)
        {
            throw new NotImplementedException();
        }

        public Task<ProductCategory> FindAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductCategory> FindByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}

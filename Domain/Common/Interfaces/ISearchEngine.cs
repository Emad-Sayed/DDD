using Domain.Base.Entity;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Interfaces
{
    public interface ISearchEngine
    {
        (List<Brand>, List<ProductCategory>, List<Product>) ListProducts();
        Task AddEntity<T>(T entity, string indexName) where T : class;
        Task UpdateEntity<T>(T entity, string indexName) where T : class;
        Task DeleteEntity(string objectId, string indexName);
    }
}

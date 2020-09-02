using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductCatalog.AggregatesModel.ProductAggregate
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Product Aggregate

    public interface IProductRepository : IRepository<Product>
    {
        Task<(int, List<Product>)> GetAllAsync(int pageNumber, int pageSize, string keyWord, string brandId, string productCategoryId);
        Task<List<Unit>> GetProductsUnits(List<string> productsIds);
        Product Add(Product product);
        void DeleteAll();
        void AddRange(List<Product> product);
        void Update(Product product);
        void Delete(Product product);
        Task<Product> FindByIdAsync(string id);
        Task<Brand> GetBrandById(string brandId);
        Task<ProductCategory> GetProductCategoryById(string productCategoryId);
    }
}

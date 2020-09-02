using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the ProductCategory Aggregate

    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        ProductCategory Add(ProductCategory productCategory);
        void AddRange(List<ProductCategory> productCategories);
        void Update(ProductCategory productCategory);
        void Delete(ProductCategory productCategory);
        void DeleteAll();
        Task<ProductCategory> FindByIdAsync(string productCategoryId);
        Task<(int, List<ProductCategory>)> GetAllProductCategorys();
        Task<(int, List<ProductCategory>)> GetProductCategorys(int pageNumber, int pageSize, string keyWord);
    }
}

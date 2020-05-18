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
        ProductCategory Update(ProductCategory productCategory);
        Task<ProductCategory> FindAsync(string id);
        Task<ProductCategory> FindByIdAsync(string id);
        Task<(int, List<ProductCategory>)> GetAllProductCategorys();
    }
}

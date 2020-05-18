using Domain.Common.Interfaces;
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
        Product Add(Product product);
        void Update(Product product);
        Task<Product> FindByIdAsync(string id);
    }
}

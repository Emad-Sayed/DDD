using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductCatalog.AggregatesModel.BrandAggregate
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Brand Aggregate

    public interface IBrandRepository : IRepository<Brand>
    {
        Brand Add(Brand brand);
        Brand Update(Brand brand);
        Task<Brand> FindAsync(string id);
        Task<Brand> FindByIdAsync(string id);
    }
}

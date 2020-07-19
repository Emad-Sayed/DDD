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
        void Update(Brand brand);
        void Delete(Brand brand);
        Task<Brand> FindByIdAsync(string brandId);
        Task<(int, List<Brand>)> GetAllBrands();
        Task<(int, List<Brand>)> GetBrands(int pageNumber, int pageSize, string keyWord);
    }
}

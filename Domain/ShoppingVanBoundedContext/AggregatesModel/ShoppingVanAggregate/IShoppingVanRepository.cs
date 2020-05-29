using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate
{
    public interface IShoppingVanRepository : IRepository<Van>
    {
        Van Add(Van van);
        Task<Van> GetCustomerShoppingVan(string customerId);
        void Update(Van van);
        void Delete(Van van);
        Task<Van> FindByIdAsync(string id);
        Task<Van> FindByCustomerIdAsync(string id);
    }
}

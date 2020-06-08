using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CustomerManagment.AggregatesModel.CustomerAggregate
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer Add(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
        Task<Customer> FindByIdAsync(string id);
        Task<Customer> GetCustomerByAccountId(string id);
        Task<(int, List<Customer>)> GetAllAsync(int pageNumber, int pageSize, string keyWord);
    }
}

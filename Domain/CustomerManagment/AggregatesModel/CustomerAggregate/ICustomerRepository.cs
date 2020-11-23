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
        Task<City> FindCityByIdAsync(string cityId);
        Task<bool> CityExistAsync(string name);
        Task<Customer> FindByIdAsync(string id);
        Task<Customer> GetCustomerByAccountId(string id);
        Task<(int, List<Customer>)> GetAllAsync(int pageNumber, int pageSize, string keyWord, string cityId, string areaId);
        Task<(int, List<City>)> GetAllCitiesAsync(int pageNumber, int pageSize, string keyWord);
        Task<Area> FindAreaById(string areaId);
        City AddCity(City city);
        void UpdateCity(City city);
        void DeleteCity(City city);
        Task<string> GetCustomerDevicesID(string accountId);
    }
}

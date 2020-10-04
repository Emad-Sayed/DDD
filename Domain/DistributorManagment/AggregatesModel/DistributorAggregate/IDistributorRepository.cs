using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DistributorManagment.AggregatesModel.DistributorAggregate
{
    public interface IDistributorRepository : IRepository<Distributor>
    {
        Distributor Add(Distributor distributor);
        void Update(Distributor distributor);
        void Delete(Distributor distributor);
        Task<City> FindCityByIdAsync(string cityId);
        Task<bool> CityExistAsync(string name);
        Task<Distributor> FindByIdAsync(string id);
        Task<(int, List<Distributor>)> GetAllAsync(int pageNumber, int pageSize, string keyWord);
        Task<List<Distributor>> GetDistributorsInAreaAsync(string areaId);
        Task<(int, List<City>)> GetAllCitiesAsync(int pageNumber, int pageSize, string keyWord);
        Task ConfirmDistributorUserEmail(string userId);
        Task<Area> FindAreaById(string areaId);
        City AddCity(City city);
        void UpdateCity(City city);
        void DeleteCity(City city);
    }
}

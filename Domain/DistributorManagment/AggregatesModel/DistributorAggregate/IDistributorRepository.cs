using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DistributorManagment.AggregatesModel.DistributorAggregate
{
    public interface IDistributorRepository: IRepository<Distributor>
    {
        Distributor Add(Distributor distributor);
        void Update(Distributor distributor);
        void Delete(Distributor distributor);
        Task<Distributor> FindByIdAsync(string id);
        Task<Distributor> GetDistributorByAccountId(string id);
        Task<(int, List<Distributor>)> GetAllAsync(int pageNumber, int pageSize, string keyWord);
    }
}

using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OffersManagment.AggregatesModel
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Offer Aggregate

    public interface IOfferRepository : IRepository<Offer>
    {
        Task<(int, List<Offer>)> GetAllAsync(int pageNumber, int pageSize, string keyWord);
        Offer Add(Offer offer);
        void Update(Offer offer);
        void UpdateAll(List<Offer> offers);
        void Delete(Offer offer);
        Task<Offer> FindByIdAsync(string id);
        Task<List<Offer>> FindByIdsAsync(List<string> ids);
    }
}

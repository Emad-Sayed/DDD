using Domain.Common.Interfaces;
using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using Microsoft.EntityFrameworkCore;
using Persistence.DistributorManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account.Usage.Record;

namespace Infrastructure.Repositories.DistributorManagment
{
    public class DistributorRepository
    : IDistributorRepository
    {
        private readonly DistributorManagmentContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public DistributorRepository(DistributorManagmentContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Distributor Add(Distributor distributor)
        {
            return _context.Distributors
                   .Add(distributor)
                   .Entity;
        }

        public void Update(Distributor distributor)
        {
            _context.Entry(distributor).State = EntityState.Modified;
        }

        public async Task<Distributor> FindByIdAsync(string id)
        {
            return await _context.Distributors
                .Include(x => x.DistributorAreas)
                .ThenInclude(x => x.Area)
                .ThenInclude(x => x.City)
                .Include(x => x.DistributorUsers)
                   .FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        public async Task<City> FindCityByIdAsync(string cityId)
        {
            return await _context.DistributorsCities
                .Include(x => x.Areas)
                   .FirstOrDefaultAsync(x => x.Id.ToLower() == cityId.ToLower());
        }

        public async Task<bool> CityExistAsync(string name)
        {
            return await _context.DistributorsCities
                   .AnyAsync(x => x.Name.ToLower() == name.ToLower());
        }

        public async Task<(int, List<Distributor>)> GetAllAsync(int pageNumber, int pageSize, string keyWord)
        {
            var query = _context.Distributors
                //.Include(x => x.DistributorItems)
                .AsQueryable();

            // fillter by keyword
            if (!string.IsNullOrEmpty(keyWord))
            {
                query = query.Where(x =>
                x.Id.ToString().Contains(keyWord)
                );
            }

            // apply pagination to products
            var products = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = query.Count();

            return (count, products);
        }

        public void Delete(Distributor distributor)
        {
            _context.Distributors.Remove(distributor);
        }

        public async Task<(int, List<City>)> GetAllCitiesAsync(int pageNumber, int pageSize, string keyWord)
        {
            var query = _context.DistributorsCities
                .Include(x => x.Areas)
                .AsQueryable();

            // fillter by keyword
            if (!string.IsNullOrEmpty(keyWord))
            {
                keyWord = keyWord.ToLower();

                query = query.Where(x =>
                x.Id.Contains(keyWord) ||
                x.Name.Contains(keyWord));
            }

            query = query.OrderBy(x => x.Name);

            // apply pagination to cities
            var cities = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = query.Count();

            return (count, cities);
        }

        public async Task ConfirmDistributorUserEmail(string userId)
        {
            var distributorUser = await _context.DistributorUsers.FirstOrDefaultAsync(x => x.AccountId == userId);
            if (distributorUser != null)
            {
                distributorUser.ConfirmEmail();
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Area> FindAreaById(string areaId)
        {
            return await _context.DistributorsAreas.FirstOrDefaultAsync(z => z.Id.ToLower() == areaId.ToLower());
        }

        public City AddCity(City city)
        {
            return _context.DistributorsCities
                     .Add(city)
                     .Entity;
        }

        public void UpdateCity(City city)
        {
            _context.Entry(city).State = EntityState.Modified;
        }

        public void DeleteCity(City city)
        {
            _context.DistributorsCities.Remove(city);
        }
    }
}

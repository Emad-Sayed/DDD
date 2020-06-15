using Domain.Common.Interfaces;
using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using Microsoft.EntityFrameworkCore;
using Persistence.DistributorManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .Include(x => x.DistributorUsers)
                   .FirstOrDefaultAsync(x => x.Id.ToString() == id);
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
            var query = _context.Cities
                .Include(x => x.Areas)
                .AsQueryable();

            // fillter by keyword
            if (!string.IsNullOrEmpty(keyWord))
            {
                query = query.Where(x =>
                x.Id.ToString().Contains(keyWord)
                );
            }

            // apply pagination to cities
            var cities = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = query.Count();

            return (count, cities);
        }
    }
}

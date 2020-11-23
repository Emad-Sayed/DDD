using Domain.Common.Interfaces;
using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using Domain.CustomerManagment.Exceptions;
using Microsoft.EntityFrameworkCore;
using Persistence.CustomerManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.CustomerManagment
{
    public class CustomerRepository
    : ICustomerRepository
    {
        private readonly CustomerManagmentContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public CustomerRepository(CustomerManagmentContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Customer Add(Customer customer)
        {
            return _context.Customers
                   .Add(customer)
                   .Entity;
        }

        public void Update(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
        }

        public async Task<City> FindCityByIdAsync(string cityId)
        {
            return await _context.CustomersCities
                .Include(x => x.Areas)
                   .FirstOrDefaultAsync(x => x.Id.ToLower() == cityId.ToLower());
        }

        public async Task<bool> CityExistAsync(string name)
        {
            return await _context.CustomersCities
                   .AnyAsync(x => x.Name.ToLower() == name.ToLower());
        }

        public async Task<Customer> FindByIdAsync(string id)
        {
            return await _context.Customers
                .Include(x => x.Area)
                .ThenInclude(x => x.City)
                   .FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        public async Task<(int, List<Customer>)> GetAllAsync(int pageNumber, int pageSize, string keyWord, string cityId, string areaId)
        {
            var query = _context.Customers
                .Include(x => x.Area)
                .ThenInclude(x => x.City)
                .AsQueryable();

            // fillter by city 
            if (!string.IsNullOrEmpty(cityId))
            {
                query = query.Where(x => x.Area.City.Id.ToLower() == cityId.ToLower());
            }

            // fillter by area 
            if (!string.IsNullOrEmpty(areaId))
            {
                query = query.Where(x => x.AreaId.ToLower() == areaId.ToLower());
            }

            // fillter by keyword
            if (!string.IsNullOrEmpty(keyWord))
            {
                query = query.Where(x =>
                x.Id.ToString().Contains(keyWord) ||
                x.CustomerCode.Contains(keyWord)
                );
            }

            // apply pagination to products
            var products = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = query.Count();

            return (count, products);
        }

        public void Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public async Task<Customer> GetCustomerByAccountId(string id)
        {
            return await _context.Customers
                .Include(x => x.Area)
                .ThenInclude(x => x.City)
                   .FirstOrDefaultAsync(x => x.AccountId == id);
        }

        public async Task<(int, List<City>)> GetAllCitiesAsync(int pageNumber, int pageSize, string keyWord)
        {
            var query = _context.CustomersCities
                .Include(x => x.Areas)
                .AsQueryable();

            // fillter by keyword
            if (!string.IsNullOrEmpty(keyWord))
            {
                keyWord = keyWord.ToLower();
                query = query.Where(x =>
                x.Name.Contains(keyWord) ||
                x.Id.Contains(keyWord)
                );
            }

            // apply pagination to cities
            var cities = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = query.Count();

            return (count, cities);
        }

        public async Task<Area> FindAreaById(string areaId)
        {
            return await _context.CustomersAreas.FirstOrDefaultAsync(z => z.Id.ToLower() == areaId.ToLower());
        }

        public City AddCity(City city)
        {
            return _context.CustomersCities
                     .Add(city)
                     .Entity;
        }

        public void UpdateCity(City city)
        {
            _context.Entry(city).State = EntityState.Modified;
        }

        public void DeleteCity(City city)
        {
            _context.CustomersCities.Remove(city);
        }

        public async Task<string> GetCustomerDevicesID(string customerId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.AccountId == customerId);
            if (customer == null) throw new CustomerNotFoundException(customerId);

            return customer.DevicesId;
        }
    }
}

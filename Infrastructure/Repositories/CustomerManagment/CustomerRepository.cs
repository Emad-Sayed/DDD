using Domain.Common.Interfaces;
using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
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

        public async Task<Customer> FindByIdAsync(string id)
        {
            return await _context.Customers
                   .FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        public async Task<(int, List<Customer>)> GetAllAsync(int pageNumber, int pageSize, string keyWord)
        {
            var query = _context.Customers
                //.Include(x => x.CustomerItems)
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

        public void Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public async Task<Customer> GetCustomerByAccountId(string id)
        {
            return await _context.Customers
                   .FirstOrDefaultAsync(x => x.AccountId == id);
        }

        public async Task<(int, List<City>)> GetAllCitiesAsync(int pageNumber, int pageSize, string keyWord)
        {
            var query = _context.Cities
                .Include(x => x.Regions)
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

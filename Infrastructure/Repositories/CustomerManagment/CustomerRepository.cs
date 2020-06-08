using Domain.Common.Interfaces;
using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Persistence.CustomerManagment;
using System;
using System.Collections.Generic;
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

        public void Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public async Task<Customer> GetCustomerByAccountId(string id)
        {
            return await _context.Customers
                   .FirstOrDefaultAsync(x => x.AccountId == id);
        }
    }
}

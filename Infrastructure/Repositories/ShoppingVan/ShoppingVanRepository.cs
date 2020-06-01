using Domain.Common.Interfaces;
using Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate;
using Microsoft.EntityFrameworkCore;
using Persistence.ShoppingVan;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ShoppingVan
{
    public class ShoppingVanRepository
    : IShoppingVanRepository
    {
        private readonly ShoppingVanContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public ShoppingVanRepository(ShoppingVanContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Van Add(Van shoppingVan)
        {
            return _context.ShoppingVans
                   .Add(shoppingVan)
                   .Entity;
        }

        public void Update(Van shoppingVan)
        {
            _context.Entry(shoppingVan).State = EntityState.Modified;
        }

        public async Task<Van> FindByIdAsync(string id)
        {
            return await _context.ShoppingVans
                   .Include(x => x.ShoppingVanItems)
                   .FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        public void Delete(Van shoppingVan)
        {
            _context.ShoppingVans.Remove(shoppingVan);
        }

        public async Task<Van> GetCustomerShoppingVan(string customerId)
        {
            var van = await _context.ShoppingVans
                .Include(x => x.ShoppingVanItems)
                .FirstOrDefaultAsync(x => x.CustomerId == customerId);
            return van;
        }

        public async Task<Van> FindByCustomerIdAsync(string id)
        {
            var customerVan = await _context.ShoppingVans
              .Include(x => x.ShoppingVanItems)
              .FirstOrDefaultAsync(x => x.CustomerId == id);
            return customerVan;
        }
    }
}

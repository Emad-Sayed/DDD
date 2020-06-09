using Domain.Common.Interfaces;
using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Persistence.OrderManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.OrderManagment
{
    public class OrderRepository
        : IOrderRepository
    {
        private readonly OrderContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public OrderRepository(OrderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Order Add(Order order)
        {
            return _context.Orders
                   .Add(order)
                   .Entity;
        }

        public async Task<(int, List<Order>)> GetAllAsync(int pageNumber, int pageSize, string keyWord)
        {
            var query = _context.Orders
                //.Include(x => x.OrderItems)
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

        public async Task<(int, List<Order>)> GetCustomerOrders(string customerId, List<OrderStatus> orderStatuses, int pageNumber, int pageSize, string keyWord)
        {
            var query = _context.Orders
                .Where(x => x.CustomerId == customerId)
                .AsQueryable();

            if (orderStatuses == null) orderStatuses = new List<OrderStatus> { OrderStatus.Placed };
            query = query.Where(x => orderStatuses.Contains(x.OrderStatus));

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

        public void Update(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
        }


        public async Task<Order> GetByIdAsync(string id)
        {
            return await _context.Orders
                   .Include(x => x.OrderItems)
                   .FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }
    }
}

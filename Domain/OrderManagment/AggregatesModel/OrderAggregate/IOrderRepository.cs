using Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrderManagment.AggregatesModel.OrderAggregate
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Order Aggregate

    public interface IOrderRepository : IRepository<Order>
    {
        Order Add(Order order);

        void Update(Order order);
        Task<(int, List<Order>)> GetAllAsync(int pageNumber, int pageSize, string keyWord);
        Task<Order> GetByIdAsync(string orderId);
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Application.CustomerManagment
{
    public interface ICustomerManagmentContext
    {
        public DbSet<Domain.CustomerManagment.Customer.DomainModels.Customer> Customers { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());

        int SaveChanges();
    }
}

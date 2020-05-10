using Application.CustomerManagment;
using Domain.CustomerManagment.Customer.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.CustomerManagment
{

    public class CustomerManagmentContext : DbContext, ICustomerManagmentContext
    {
        public CustomerManagmentContext(DbContextOptions<CustomerManagmentContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerManagmentContext).Assembly, type => type.FullName.Contains("CustomerManagment"));
        }
    }
}

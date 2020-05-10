using Application.Common.Interfaces;
using Application.CustomerManagment;
using Application.ProductCatalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.CustomerManagment;
using Persistence.ProductCatalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public class Startup : IStartup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProductCatalogContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("BrimoDatabase"),
                    o => o.MigrationsHistoryTable("_ProductCatalog_MigrationHistory")));

            services.AddScoped<IProductCatalogContext>(provider => provider.GetService<ProductCatalogContext>());

            services.AddDbContext<CustomerManagmentContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("BrimoDatabase"),
                   o => o.MigrationsHistoryTable("_CustomerManagment_MigrationHistory")));

            services.AddScoped<ICustomerManagmentContext>(provider => provider.GetService<CustomerManagmentContext>());


        }

        public void Configure(IServiceProvider provider)
        {

        }
    }
}

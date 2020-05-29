﻿using Application.Common.Interfaces;
using Application.CustomerManagment;
using Application.ProductCatalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.CustomerManagment;
using Persistence.Order;
using Persistence.ProductCatalog;
using Persistence.ShoppingVan;
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
                    o => o.MigrationsHistoryTable("_ProductCatalog_MigrationHistory")), ServiceLifetime.Scoped);

            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BrimoDatabase"),
                o => o.MigrationsHistoryTable("_Order_MigrationHistory")), ServiceLifetime.Scoped);

            services.AddDbContext<ShoppingVanContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BrimoDatabase"),
                o => o.MigrationsHistoryTable("_ShoppingVan_MigrationHistory")), ServiceLifetime.Scoped);

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

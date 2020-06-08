using Application.Common.Interfaces;
using Application.CustomerManagment;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.BlobStorage.AzureBS;
using Persistence.BlobStorage.AzureBS.Repository;
using Persistence.CustomerManagment;
using Persistence.DistributorManagment;
using Persistence.OrderManagment;
using Persistence.ProductCatalog;
using Persistence.ShoppingVan;
using System;

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

            services.AddDbContext<DistributorManagmentContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("BrimoDatabase"),
                   o => o.MigrationsHistoryTable("_DistributorManagment_MigrationHistory")));


            AzureConfigurations azureConfigurations = new AzureConfigurations();
            Configuration.Bind("AzureConfigurations", azureConfigurations);
            services.AddSingleton(azureConfigurations);

            services.AddSingleton<IPhotoRepository, PhotoRepository>();
        }

        public void Configure(IServiceProvider provider)
        {

        }
    }
}

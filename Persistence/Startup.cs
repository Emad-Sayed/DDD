using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                 options.UseSqlServer(Configuration.GetConnectionString("BrimoDatabase"), o => o.MigrationsHistoryTable("_ProductCatalog_MigrationHistory")));

        }

        public void Configure(IServiceProvider provider)
        {

        }
    }
}

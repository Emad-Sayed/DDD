using Application.Common.Interfaces;
using Domain.Common.Interfaces;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using Infrastructure.Repositories.ProductCatalog;
using Infrastructure.SearchEngine;
using Infrastructure.SMSMessage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure
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
            var twilioSMSConfigurations = Configuration.GetSection(nameof(TwilioSMSConfigurations)).Get<TwilioSMSConfigurations>();
            services.AddSingleton(twilioSMSConfigurations);

            services.AddTransient<ISMSNotification, TwilioSMSNotification>();

            var algoliaSearchEngineConfigurations = Configuration.GetSection(nameof(AlgoliaSearchEngineConfigurations)).Get<AlgoliaSearchEngineConfigurations>();
            services.AddSingleton(algoliaSearchEngineConfigurations);

            services.AddTransient<ISearchEngine, AlgoliaSearchEngine>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

        }

        public void Configure(IServiceProvider provider)
        {
            //throw new NotImplementedException();
        }
    }
}

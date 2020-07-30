using Application.Common.Interfaces;
using Domain.Common.Interfaces;
using Domain.CustomerManagment.AggregatesModel.CustomerAggregate;
using Domain.DistributorManagment.AggregatesModel.DistributorAggregate;
using Domain.OffersManagment.AggregatesModel;
using Domain.OrderManagment.AggregatesModel.OrderAggregate;
using Domain.ProductCatalog.AggregatesModel.BrandAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductAggregate;
using Domain.ProductCatalog.AggregatesModel.ProductCategoryAggregate;
using Domain.ShoppingVanBoundedContext.AggregatesModel.ShoppingVanAggregate;
using Infrastructure.Repositories.CustomerManagment;
using Infrastructure.Repositories.DistributorManagment;
using Infrastructure.Repositories.OfferManagment;
using Infrastructure.Repositories.OrderManagment;
using Infrastructure.Repositories.ProductCatalog;
using Infrastructure.Repositories.ShoppingVan;
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
            services.AddTransient<IShoppingVanRepository, ShoppingVanRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IDistributorRepository, DistributorRepository>();
            services.AddTransient<IOfferRepository, OfferRepository>();

        }

        public void Configure(IServiceProvider provider)
        {
            //throw new NotImplementedException();
        }
    }
}

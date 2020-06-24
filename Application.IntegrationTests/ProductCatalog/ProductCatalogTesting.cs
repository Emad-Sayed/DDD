using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Moq;
using NUnit.Framework;
using Persistence.ProductCatalog;
using Respawn;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Application.IntegrationTests.ProductCatalog
{
    [SetUpFixture]
    public class ProductCatalogTesting
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpoint;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var startup = new API.Startup(_configuration);

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IHostingEnvironment>(w =>
                w.EnvironmentName == "Development"));


            services.AddLogging();

            startup.ConfigureServices(services);

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory", "Brands" },
                
            };

        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        public static async Task ResetState()
        {
            await _checkpoint.Reset(_configuration.GetConnectionString("BrimoDatabase"));
        }

        public static async Task<T> FindAsync<T>(string id)
            where T : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ProductCatalogContext>();

            return await context.FindAsync<T>(new Guid(id));
        }

        public static async Task<T> CreateAsync<T>(T entity)
     where T : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ProductCatalogContext>();

            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
        }
    }
}
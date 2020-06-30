using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Brimo.IDP.Admin.EntityFramework.Shared.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Persistence.OrderManagment;
using Respawn;

namespace Application.IntegrationTests.OrderManagment
{
    [SetUpFixture]
    public class OrderManagmentTesting
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpoint;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables();

            
            builder.AddJsonFile("appsettings.Testing.json", true, true);
            builder.AddJsonFile("appsettings.Testing.Development.json", true, true);


            _configuration = builder.Build();

            var startup = new API.Startup(_configuration);
            var identityStartUp = new Brimo.IDP.STS.Identity.Startup(_configuration);

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "API"));

            services.AddSingleton(Mock.Of<IHostingEnvironment>(w =>
                            w.EnvironmentName == "Development" &&
                            w.ApplicationName == "API"));

            services.AddLogging();

            startup.ConfigureServices(services);
            identityStartUp.ConfigureServices(services);

            // Setup testing user (need to add a user to identity and use a real guid)
            var currentUserServiceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(ICurrentUserService));

            services.Remove(currentUserServiceDescriptor);

            services.AddTransient<ICurrentUserService, CurrentUserService>();

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory" }
            };
            EnsureDatabase();
        }

        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<OrderContext>();
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        private class CurrentUserService : ICurrentUserService
        {
            public string UserId => _currentUserId;

            public string Address => throw new NotImplementedException();
        }

        private static string _currentUserId;

        public static async Task<string> RunAsDefaultUserAsync()
        {
            return await RunAsUserAsync("test@local", "Testing1234!");
        }

        public static async Task<string> RunAsUserAsync(string userName, string password)
        {
            using var scope = _scopeFactory.CreateScope();

            var userManager = scope.ServiceProvider.GetService<UserManager<UserIdentity>>();

            var user = new UserIdentity { UserName = userName, Email = userName };

            var result = await userManager.CreateAsync(user, password);

            _currentUserId = user.Id;

            return _currentUserId;
        }

        public static async Task ResetState()
        {
            await _checkpoint.Reset(_configuration.GetConnectionString("BrimoDatabase"));
            _currentUserId = null;
        }

        public static async Task<T> FindAsync<T>(string id)
            where T : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<OrderContext>();

            return await context.FindAsync<T>(new Guid(id));
        }

        public static async Task<T> CreateAsync<T>(T entity)
     where T : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<OrderContext>();

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

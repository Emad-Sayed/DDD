using API.Configuration;
using IdentityModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public static class StartupHelpers
    {
        public static void AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationConsts.AdministrationPolicy,
                    policy => policy.RequireAssertion(context => context.User.IsInRole("Admin")));

                options.AddPolicy(AuthorizationConsts.DistributorPolicy,
                    policy => policy.RequireAssertion(context => context.User.IsInRole("Distributor")));

                options.AddPolicy(AuthorizationConsts.CustomerPolicy,
                    policy => policy.RequireAssertion(context => context.User.IsInRole("Customer")));

            });
        }
    }
}

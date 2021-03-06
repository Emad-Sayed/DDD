using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.Controllers;
using API.Helpers;
using API.Services;
using Application.Common.Interfaces;
using Application.Common.Middlewares;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        private readonly List<IStartup> _assembliesStartup;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _assembliesStartup = new List<IStartup>
            {
                new Application.Startup(Configuration),
                new Persistence.Startup(Configuration),
                new Infrastructure.Startup(Configuration)
            };
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins(
                                          "http://brimo-dev-identity-brimowebui.azurewebsites.net",
                                          "http://localhost:4200",
                                          "http://localhost:4210",
                                          "http://localhost:2021"
                                          )
                                      .AllowAnyHeader().AllowAnyMethod();
                                  });
            });
            services.AddControllers().AddNewtonsoftJson(options =>
                     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
          );
            services.AddMvcCore().AddAuthorization();
            // Note - this is on the IMvcBuilder, not the service collection
            services.AddAuthorizationPolicies();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Contexts.CustomerManagment, new OpenApiInfo { Title = Contexts.CustomerManagment, Version = "v1" });
            });


            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();

            _assembliesStartup.ForEach(startup => startup.ConfigureServices(services));

            //services.AddApplicationInsightsTelemetry(Configuration["ApplicationInsights:InstrumentationKey"]);

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(option =>
            {
                option.Authority = Configuration["IdentityServerAddress"];
                option.Audience = "brimo_api";

                option.RequireHttpsMetadata = false;
            });
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            var ss = env.EnvironmentName;
            app.UseMiddleware<ErrorHandlingMiddleware>();
            _assembliesStartup.ForEach(x => x.Configure(app.ApplicationServices));
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{Contexts.CustomerManagment}/swagger.json", Contexts.CustomerManagment);
            });

            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

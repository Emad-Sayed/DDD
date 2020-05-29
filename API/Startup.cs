using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.Controllers;
using API.Services;
using Application.Common.Interfaces;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
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
            services.AddControllers();
            services.AddMvcCore()
                .AddAuthorization();
            // Note - this is on the IMvcBuilder, not the service collection
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Contexts.CustomerManagment, new OpenApiInfo { Title = Contexts.CustomerManagment, Version = "v1" });
            });


            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();

            _assembliesStartup.ForEach(startup => startup.ConfigureServices(services));
            services.AddCors(x => x.AddPolicy("AllowOrigin", o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

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
                option.Authority = "http://localhost:5000";
                option.Audience = "brimo_api";
                
                option.RequireHttpsMetadata = false;
            });
            //.AddIdentityServerAuthentication(options =>
            //{
            //    options.Authority = "http://localhost:5000";
            //    options.RequireHttpsMetadata = false;
            //    options.ApiName = "brimo_api";
            //});
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            _assembliesStartup.ForEach(x => x.Configure(app.ApplicationServices));
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{Contexts.CustomerManagment}/swagger.json", Contexts.CustomerManagment);
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseCors("AllowOrigin");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

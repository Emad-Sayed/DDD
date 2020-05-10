using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Contexts.CustomerManagment, new OpenApiInfo { Title = Contexts.CustomerManagment, Version = "v1" });
            });

            _assembliesStartup.ForEach(startup => startup.ConfigureServices(services));
            services.AddCors(x => x.AddPolicy("AllowOrigin", o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "http://localhost:5000";
                options.RequireHttpsMetadata = false;

                options.Audience = "brimo_api";
            });
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            _assembliesStartup.ForEach(x=>x.Configure(app.ApplicationServices));
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{Contexts.CustomerManagment}/swagger.json", Contexts.CustomerManagment);
            });

            app.UseRouting();
            app.UseCors("AllowOrigin");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

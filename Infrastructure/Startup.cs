using Application.Common.Interfaces;
using Infrastructure.SMSMessage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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
            var SsSMessagesConfigurations = Configuration.GetSection(nameof(SMSMessagesConfigurations)).Get<SMSMessagesConfigurations>();
            services.AddSingleton(SsSMessagesConfigurations);
            services.AddTransient<ISMSMessagesService, SMSMessagesService>();
            //throw new NotImplementedException();
        }

        public void Configure(IServiceProvider provider)
        {
            //throw new NotImplementedException();
        }
    }
}

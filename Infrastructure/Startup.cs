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
            var twilioSMSConfigurations = Configuration.GetSection(nameof(TwilioSMSConfigurations)).Get<TwilioSMSConfigurations>();
            services.AddSingleton(twilioSMSConfigurations);

            services.AddTransient<ISMSNotification, TwilioSMSNotification>();
            //throw new NotImplementedException();
        }

        public void Configure(IServiceProvider provider)
        {
            //throw new NotImplementedException();
        }
    }
}

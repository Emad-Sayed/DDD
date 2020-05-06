using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common.Interfaces
{
    public interface IStartup
    {
        void ConfigureServices(IServiceCollection services);

        void Configure(IServiceProvider provider);
    }
}

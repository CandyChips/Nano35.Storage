using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Storage.Processor.Configurations;

namespace Nano35.Storage.Processor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {            
            new Configurator(services, new EntityFrameworkConfiguration("192.168.100.120", "Nano35.Storage.DB", "sa", "Cerber666")).Configure();
            new Configurator(services, new MassTransitConfiguration()).Configure();
            new Configurator(services, new AutoMapperConfiguration()).Configure();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
        }
    }
}

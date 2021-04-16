using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Storage.Processor.Configurations;

namespace Nano35.Storage.Processor
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("ServicesConfig.json");
            Configuration = builder.Build();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {            
            new Configurator(services, new EntityFrameworkConfiguration(Configuration)).Configure();
            new Configurator(services, new MassTransitConfiguration()).Configure();
            new Configurator(services, new AuthenticationConfiguration()).Configure();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) { }
    }
}

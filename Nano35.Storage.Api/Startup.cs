using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nano35.Contracts;
using Nano35.Storage.Api.Configurations;
using Nano35.Storage.Api.Middlewares;

namespace Nano35.Storage.Api
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
            new Configurator(services, new AuthenticationConfiguration(Configuration)).Configure();
            new Configurator(services, new CorsConfiguration()).Configure();
            new Configurator(services, new SwaggerConfiguration()).Configure();
            new Configurator(services, new MassTransitConfiguration()).Configure();
            new Configurator(services, new ConfigurationOfControllers()).Configure();
            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nano35.RepairOrders.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                ConfigureCommon.Configure(app);
                ConfigureEndpoints.Configure(app);
            });
        }
    }
}

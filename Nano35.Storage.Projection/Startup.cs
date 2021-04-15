using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nano35.Contracts;
using Nano35.Storage.Projection.Configurations;

namespace Nano35.Storage.Projection
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            new Configurator(services, new CorsConfiguration()).Configure();
            new Configurator(services, new SwaggerConfiguration()).Configure();
            new Configurator(services, new MassTransitConfiguration()).Configure();
            new Configurator(services, new ConfigurationOfControllers()).Configure();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nano35.Instance.Api");
            });
            
            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    context.Response.Redirect("/swagger");
                });
            });
            
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;

namespace Nano35.Storage.Api.Configurations
{
    public class ConfigurationOfAuthStateProvider : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            services.AddScoped<ICustomAuthStateProvider, CookiesAuthStateProvider>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
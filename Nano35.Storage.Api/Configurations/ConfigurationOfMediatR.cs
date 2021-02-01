using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Storage.Api.Requests.Behaviours;

namespace Nano35.Storage.Api.Configurations
{
    public class MediatRConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CacheQueryPipeLineBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggerQueryDecorator<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggerCommandDecorator<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeLineBehaviour<,>));
        }
    }
}
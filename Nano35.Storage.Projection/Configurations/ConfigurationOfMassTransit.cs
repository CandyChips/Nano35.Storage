using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Projection.Configurations
{
    public class MassTransitConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {   
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(provider);
                    cfg.Host(new Uri($"{ContractBase.RabbitMqLocation}/"), h =>
                    {
                        h.Username(ContractBase.RabbitMqUsername);
                        h.Password(ContractBase.RabbitMqPassword);
                    });
                }));
                x.AddRequestClient<IGetAllStorageItemConditionsRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetAllStorageItemConditionsRequestContract"));
                x.AddRequestClient<IPresentationGetAllArticlesRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IPresentationGetAllArticlesRequestContract"));
                x.AddRequestClient<IPresentationGetAllCategoriesRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IPresentationGetAllCategoriesRequestContract"));
                x.AddRequestClient<IPresentationGetAllStorageItemsRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IPresentationGetAllStorageItemsRequestContract"));

            });
            services.AddMassTransitHostedService();
        }
    }
}
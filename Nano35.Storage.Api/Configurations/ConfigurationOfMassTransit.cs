using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Configurations
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
                x.AddRequestClient<IGetAllArticlesRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllArticlesRequestContract"));
                x.AddRequestClient<IGetArticleByIdRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetArticleByIdRequestContract"));
                x.AddRequestClient<ICreateArticleRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateArticleRequestContract"));
                x.AddRequestClient<IGetAllStorageItemsRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllStorageItemsRequestContract"));
                x.AddRequestClient<IGetStorageItemByIdRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetStorageItemByIdRequestContract"));
                x.AddRequestClient<ICreateStorageItemRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateStorageItemRequestContract"));
                x.AddRequestClient<IGetAllArticlesBrandsSuccessResultContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllArticlesBrandsSuccessResultContract"));
                x.AddRequestClient<IGetAllArticlesModelsSuccessResultContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllArticlesModelsSuccessResultContract"));
                x.AddRequestClient<IGetAllArticlesCategoriesRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllArticlesCategoriesRequestContract"));
                x.AddRequestClient<IGetAllArticlesCategoryGroupsRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllArticlesCategoryGroupsRequestContract"));
                x.AddRequestClient<IGetAllArticleTypesRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllArticleTypesRequestContract"));
                x.AddRequestClient<IGetAllStorageItemConditionsRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllStorageItemConditionsRequestContract"));
            });
            services.AddMassTransitHostedService();
        }
    }
}
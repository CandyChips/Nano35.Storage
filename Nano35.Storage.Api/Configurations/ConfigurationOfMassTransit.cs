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
                x.AddRequestClient<ICreateCancellationRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateCancellationRequestContract"));
                x.AddRequestClient<ICreateSelleRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateSelleRequestContract"));
                x.AddRequestClient<ICreateComingRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateComingRequestContract"));
                x.AddRequestClient<ICreateMoveRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateMoveRequestContract"));
                x.AddRequestClient<ICreateCategoryRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateCategoryRequestContract"));
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
                x.AddRequestClient<IGetAllArticlesBrandsRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllArticlesBrandsRequestContract"));
                x.AddRequestClient<IGetAllArticlesCategoriesRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllArticlesCategoriesRequestContract"));
                x.AddRequestClient<IGetAllArticlesModelsRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllArticlesModelsRequestContract"));
                x.AddRequestClient<IGetAllStorageItemConditionsRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllStorageItemConditionsRequestContract"));
                x.AddRequestClient<IGetAllComingsRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllComingsRequestContract"));
                x.AddRequestClient<IGetComingDetailsByIdRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetComingDetailsByIdRequestContract"));
            });
            services.AddMassTransitHostedService();
        }
    }
}
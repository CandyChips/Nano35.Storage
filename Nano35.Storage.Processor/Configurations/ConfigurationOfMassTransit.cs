using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Storage.Processor.Consumers;

namespace Nano35.Storage.Processor.Configurations
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
                    
                    cfg.ReceiveEndpoint("IGetAllArticlesRequestContract", e =>
                    {
                        e.Consumer<GetAllArticlesConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGetArticleByIdRequestContract", e =>
                    {
                        e.Consumer<GetArticleByIdConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("ICreateArticleRequestContract", e =>
                    {
                        e.Consumer<CreateArticleConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGetAllStorageItemsRequestContract", e =>
                    {
                        e.Consumer<GetAllStorageItemsConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("IGetStorageItemByIdRequestContract", e =>
                    {
                        e.Consumer<GetStorageItemByIdConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("ICreateStorageItemRequestContract", e =>
                    {
                        e.Consumer<CreateStorageItemConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IGetAllArticlesBrandsRequestContract", e =>
                    {
                        e.Consumer<GetAllArticlesBrandsConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IGetAllArticlesCategoriesRequestContract", e =>
                    {
                        e.Consumer<GetAllArticlesCategoriesConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IGetAllArticlesModelsRequestContract", e =>
                    {
                        e.Consumer<GetAllArticlesModelsConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IGetAllStorageItemConditionsRequestContract", e =>
                    {
                        e.Consumer<GetAllStorageItemConditionsConsumer>(provider);
                    });
                }));
                x.AddConsumer<CreateArticleConsumer>();
                x.AddConsumer<CreateStorageItemConsumer>();
                x.AddConsumer<GetAllArticlesConsumer>();
                x.AddConsumer<GetArticleByIdConsumer>();
                x.AddConsumer<GetAllStorageItemsConsumer>();
                x.AddConsumer<GetStorageItemByIdConsumer>();
                x.AddConsumer<GetAllArticlesBrandsConsumer>();
                x.AddConsumer<GetAllArticlesCategoriesConsumer>();
                x.AddConsumer<GetAllArticlesModelsConsumer>();
                x.AddConsumer<GetAllStorageItemConditionsConsumer>();
                
                x.AddRequestClient<IGetUserByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetUserByIdRequestContract"));
            });
            services.AddMassTransitHostedService();
        }
    }
}
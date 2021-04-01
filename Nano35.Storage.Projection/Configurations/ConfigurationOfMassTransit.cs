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
                x.AddRequestClient<IGetAllArticlesRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllArticlesRequestContract"));
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
                x.AddRequestClient<IGetAllPlacesOfStorageItemOnInstanceContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllPlacesOfStorageItemOnInstanceRequestContract"));
                x.AddRequestClient<IGetAllPlacesOfStorageItemOnUnitRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllPlacesOfStorageItemOnUnitRequestContract"));
                x.AddRequestClient<IGetAllStorageItemsOnInstanceContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllStorageItemsOnInstanceContract"));
                x.AddRequestClient<IGetAllStorageItemsOnUnitContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllStorageItemsOnUnitContract"));
                x.AddRequestClient<IUpdateArticleBrandRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IUpdateArticleBrandRequestContract"));
                x.AddRequestClient<IUpdateArticleCategoryRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IUpdateArticleCategoryRequestContract"));
                x.AddRequestClient<IUpdateArticleInfoRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IUpdateArticleInfoRequestContract"));
                x.AddRequestClient<IUpdateArticleModelRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IUpdateArticleModelRequestContract"));
                x.AddRequestClient<IUpdateCategoryNameRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IUpdateCategoryNameRequestContract"));
                x.AddRequestClient<IUpdateCategoryParentCategoryIdRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IUpdateCategoryParentCategoryIdRequestContract"));
                x.AddRequestClient<IUpdateCategoryParentCategoryIdRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IUpdateCategoryParentCategoryIdRequestContract"));
                x.AddRequestClient<IUpdateStorageItemArticleRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IUpdateStorageItemArticleRequestContract"));
                x.AddRequestClient<IUpdateStorageItemCommentRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IUpdateStorageItemCommentRequestContract"));
                x.AddRequestClient<IUpdateStorageItemConditionRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IUpdateStorageItemConditionRequestContract"));
                x.AddRequestClient<IUpdateStorageItemHiddenCommentRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IUpdateStorageItemHiddenCommentRequestContract"));
                x.AddRequestClient<IUpdateStorageItemPurchasePriceRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IUpdateStorageItemPurchasePriceRequestContract"));
                x.AddRequestClient<IUpdateStorageItemRetailPriceRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IUpdateStorageItemRetailPriceRequestContract"));

                x.AddRequestClient<IGetAllComingsRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllComingsRequestContract"));
                x.AddRequestClient<ICreateComingRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateComingRequestContract"));
                x.AddRequestClient<IGetAllComingDetailsRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllComingDetailsRequestContract"));

                x.AddRequestClient<ICreateSelleRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateSelleRequestContract"));
                x.AddRequestClient<IGetAllSellsRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllSellsRequestContract"));
                x.AddRequestClient<IGetAllSelleDetailsRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllSelleDetailsRequestContract"));
                
                x.AddRequestClient<ICreateMoveRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateMoveRequestContract"));
                x.AddRequestClient<IGetAllMovesRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllMovesRequestContract"));
                x.AddRequestClient<IGetAllMoveDetailsRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllMoveDetailsRequestContract"));
                
                x.AddRequestClient<ICreateCancellationRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/ICreateCancellationRequestContract"));
                x.AddRequestClient<IGetAllCancellationsRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllCancellationsRequestContract"));
                x.AddRequestClient<IGetAllCancellationDetailsRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllCancellationDetailsRequestContract"));

            });
            services.AddMassTransitHostedService();
        }
    }
}
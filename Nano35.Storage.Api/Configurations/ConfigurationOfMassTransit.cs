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
                x.AddRequestClient<IGetAllCancellationsRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllCancellationsRequestContract"));
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
                x.AddRequestClient<IGetAllMovesRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllMovesRequestContract"));
                x.AddRequestClient<IGetAllSellsRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllSellsRequestContract"));
                x.AddRequestClient<IGetComingDetailsByIdRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetComingDetailsByIdRequestContract"));
                x.AddRequestClient<IGetMoveDetailsByIdRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetMoveDetailsByIdRequestContract"));
                x.AddRequestClient<IGetSelleDetailsByIdRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetSelleDetailsByIdRequestContract"));
                x.AddRequestClient<IGetAllWarehousesOfItemRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllWarehousesOfItemRequestContract"));
                x.AddRequestClient<IGetAllWarehousesOfUnitRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllWarehousesOfUnitRequestContract"));
                x.AddRequestClient<IGetCancellationDetailsByIdRequestContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetCancellationDetailsByIdRequestContract"));
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
                x.AddRequestClient<IGetAllPlacesOnStorageContract>(
                    new Uri($"{ContractBase.RabbitMqLocation}/IGetAllPlacesOnStorageContract"));
            });
            services.AddMassTransitHostedService();
        }
    }
}
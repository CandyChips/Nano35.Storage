using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Contracts.Cashbox.Artifacts;
using Nano35.Contracts.files;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Storage.Processor.UseCases.CheckExistArticle;
using Nano35.Storage.Processor.UseCases.CheckExistStorageItem;
using Nano35.Storage.Processor.UseCases.CreateArticle;
using Nano35.Storage.Processor.UseCases.CreateCancellation;
using Nano35.Storage.Processor.UseCases.CreateCategory;
using Nano35.Storage.Processor.UseCases.CreateComing;
using Nano35.Storage.Processor.UseCases.CreateMove;
using Nano35.Storage.Processor.UseCases.CreateSalle;
using Nano35.Storage.Processor.UseCases.CreateStorageItem;
using Nano35.Storage.Processor.UseCases.GetAllArticleBrands;
using Nano35.Storage.Processor.UseCases.GetAllArticleCategories;
using Nano35.Storage.Processor.UseCases.GetAllArticleModels;
using Nano35.Storage.Processor.UseCases.GetAllArticles;
using Nano35.Storage.Processor.UseCases.GetAllCancellationDetails;
using Nano35.Storage.Processor.UseCases.GetAllCancellations;
using Nano35.Storage.Processor.UseCases.GetAllComingDetails;
using Nano35.Storage.Processor.UseCases.GetAllComings;
using Nano35.Storage.Processor.UseCases.GetAllMoveDetails;
using Nano35.Storage.Processor.UseCases.GetAllMoves;
using Nano35.Storage.Processor.UseCases.GetAllPlacesOfStorageItemOnInstance;
using Nano35.Storage.Processor.UseCases.GetAllPlacesOfStorageItemOnUnit;
using Nano35.Storage.Processor.UseCases.GetAllSelleDetails;
using Nano35.Storage.Processor.UseCases.GetAllSells;
using Nano35.Storage.Processor.UseCases.GetAllStorageItemConditions;
using Nano35.Storage.Processor.UseCases.GetAllStorageItems;
using Nano35.Storage.Processor.UseCases.GetAllStorageItemsOnInstance;
using Nano35.Storage.Processor.UseCases.GetAllStorageItemsOnUnit;
using Nano35.Storage.Processor.UseCases.GetArticleById;
using Nano35.Storage.Processor.UseCases.GetCancellationById;
using Nano35.Storage.Processor.UseCases.GetComingById;
using Nano35.Storage.Processor.UseCases.GetSelleById;
using Nano35.Storage.Processor.UseCases.GetStorageItemById;
using Nano35.Storage.Processor.UseCases.PresentationGetAllArticles;
using Nano35.Storage.Processor.UseCases.PresentationGetAllCategories;
using Nano35.Storage.Processor.UseCases.PresentationGetAllStorageItems;
using Nano35.Storage.Processor.UseCases.UpdateArticleBrand;
using Nano35.Storage.Processor.UseCases.UpdateArticleCategory;
using Nano35.Storage.Processor.UseCases.UpdateArticleInfo;
using Nano35.Storage.Processor.UseCases.UpdateArticleModel;
using Nano35.Storage.Processor.UseCases.UpdateCategoryName;
using Nano35.Storage.Processor.UseCases.UpdateCategoryParentCategoryId;
using Nano35.Storage.Processor.UseCases.UpdateStorageItemArticle;
using Nano35.Storage.Processor.UseCases.UpdateStorageItemComment;
using Nano35.Storage.Processor.UseCases.UpdateStorageItemCondition;
using Nano35.Storage.Processor.UseCases.UpdateStorageItemHiddenComment;
using Nano35.Storage.Processor.UseCases.UpdateStorageItemPurchasePrice;
using Nano35.Storage.Processor.UseCases.UpdateStorageItemRetailPrice;

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
                    
                    cfg.ReceiveEndpoint("IGetAllArticlesRequestContract", e => { e.Consumer<GetAllArticlesConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetArticleByIdRequestContract", e => { e.Consumer<GetArticleByIdConsumer>(provider); });
                    cfg.ReceiveEndpoint("ICreateArticleRequestContract", e => { e.Consumer<CreateArticleConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllStorageItemsRequestContract", e => { e.Consumer<GetAllStorageItemsConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetStorageItemByIdRequestContract", e => { e.Consumer<GetStorageItemByIdConsumer>(provider); });
                    cfg.ReceiveEndpoint("ICreateStorageItemRequestContract", e => { e.Consumer<CreateStorageItemConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllArticlesBrandsRequestContract", e => { e.Consumer<GetAllArticlesBrandsConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllArticlesModelsRequestContract", e => { e.Consumer<GetAllArticlesModelsConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllStorageItemConditionsRequestContract", e => { e.Consumer<GetAllStorageItemConditionsConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllStorageItemsOnInstanceRequestContract", e => { e.Consumer<GetAllStorageItemsOnInstanceConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllPlacesOfStorageItemOnUnitRequestContract", e => { e.Consumer<GetAllPlacesOfStorageItemOnUnitConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllStorageItemsOnUnitContract", e => { e.Consumer<GetAllStorageItemsOnUnitConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateArticleBrandRequestContract", e => { e.Consumer<UpdateArticleBrandConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateArticleCategoryRequestContract", e => { e.Consumer<UpdateArticleCategoryConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateArticleInfoRequestContract", e => { e.Consumer<UpdateArticleInfoConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateArticleModelRequestContract", e => { e.Consumer<UpdateArticleModelConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateStorageItemArticleRequestContract", e => { e.Consumer<UpdateStorageItemArticleConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateStorageItemCommentRequestContract", e => { e.Consumer<UpdateStorageItemCommentConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateStorageItemConditionRequestContract", e => { e.Consumer<UpdateStorageItemConditionConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateStorageItemHiddenCommentRequestContract", e => { e.Consumer<UpdateStorageItemHiddenCommentConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateStorageItemPurchasePriceRequestContract", e => { e.Consumer<UpdateStorageItemPurchasePriceConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateStorageItemRetailPriceRequestContract", e => { e.Consumer<UpdateStorageItemRetailPriceConsumer>(provider); });
                    cfg.ReceiveEndpoint("GetAllPlacesOfStorageItemOnInstanceContract", e => { e.Consumer<GetAllPlacesOfStorageItemOnInstanceConsumer>(provider); });
                    
                    cfg.ReceiveEndpoint("IGetAllArticlesCategoriesRequestContract", e => { e.Consumer<GetAllArticlesCategoriesConsumer>(provider); });
                    cfg.ReceiveEndpoint("ICreateCategoryRequestContract", e => { e.Consumer<CreateCategoryConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateCategoryNameRequestContract", e => { e.Consumer<UpdateCategoryNameConsumer>(provider); });
                    cfg.ReceiveEndpoint("IUpdateCategoryParentCategoryIdRequestContract", e => { e.Consumer<UpdateCategoryParentCategoryIdConsumer>(provider); });
                    
                    cfg.ReceiveEndpoint("ICreateSelleRequestContract", e => { e.Consumer<CreateSelleConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllSellsRequestContract", e => { e.Consumer<GetAllSellsConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetSelleByIdRequestContract", e => { e.Consumer<GetSelleByIdConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllSelleDetailsRequestContract", e => { e.Consumer<GetAllSelleDetailsConsumer>(provider); });
                    
                    cfg.ReceiveEndpoint("ICreateMoveRequestContract", e => { e.Consumer<CreateMoveConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllMovesRequestContract", e => { e.Consumer<GetAllMovesConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllMoveDetailsRequestContract", e => { e.Consumer<GetAllMoveDetailsConsumer>(provider); });
                    
                    cfg.ReceiveEndpoint("ICreateCancellationRequestContract", e => { e.Consumer<CreateCancellationConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllCancellationsRequestContract", e => { e.Consumer<GetAllCancellationsConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetCancellationByIdRequestContract", e => { e.Consumer<GetCancellationByIdConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllCancellationDetailsRequestContract", e => { e.Consumer<GetAllCancellationDetailsConsumer>(provider); });
                    
                    cfg.ReceiveEndpoint("ICreateComingRequestContract", e => { e.Consumer<CreateComingConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllComingsRequestContract", e => { e.Consumer<GetAllComingsConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetComingByIdRequestContract", e => { e.Consumer<GetComingByIdConsumer>(provider); });
                    cfg.ReceiveEndpoint("IGetAllComingDetailsRequestContract", e => { e.Consumer<GetAllComingDetailsConsumer>(provider); });
                    
                    cfg.ReceiveEndpoint("IPresentationGetAllArticlesRequestContract", e => { e.Consumer<PresentationGetAllArticlesConsumer>(provider); });
                    cfg.ReceiveEndpoint("IPresentationGetAllCategoriesRequestContract", e => { e.Consumer<PresentationGetAllCategoriesConsumer>(provider); });
                    cfg.ReceiveEndpoint("IPresentationGetAllStorageItemsRequestContract", e => { e.Consumer<PresentationGetAllStorageItemsConsumer>(provider); });
                    
                    cfg.ReceiveEndpoint("ICheckExistArticleRequestContract", e => { e.Consumer<CheckExistArticleConsumer>(provider); });
                    cfg.ReceiveEndpoint("ICheckExistStorageItemRequestContract", e => { e.Consumer<CheckExistStorageItemConsumer>(provider); });
                    
                }));
                x.AddConsumer<PresentationGetAllArticlesConsumer>();
                x.AddConsumer<PresentationGetAllCategoriesConsumer>();
                x.AddConsumer<PresentationGetAllStorageItemsConsumer>();
                
                x.AddConsumer<CreateSelleConsumer>();
                x.AddConsumer<GetAllSellsConsumer>();
                x.AddConsumer<GetSelleByIdConsumer>();
                x.AddConsumer<GetAllSelleDetailsConsumer>();
                
                x.AddConsumer<CreateComingConsumer>();
                x.AddConsumer<GetAllComingsConsumer>();
                x.AddConsumer<GetComingByIdConsumer>();
                x.AddConsumer<GetAllComingDetailsConsumer>();
                
                x.AddConsumer<CreateMoveConsumer>();
                x.AddConsumer<GetAllMovesConsumer>();
                x.AddConsumer<GetAllMoveDetailsConsumer>();
                
                x.AddConsumer<CreateCancellationConsumer>();
                x.AddConsumer<GetAllCancellationsConsumer>();
                x.AddConsumer<GetCancellationByIdConsumer>();
                x.AddConsumer<GetAllCancellationDetailsConsumer>();
                
                x.AddConsumer<GetAllPlacesOfStorageItemOnUnitConsumer>();
                x.AddConsumer<GetAllStorageItemsOnUnitConsumer>();
                x.AddConsumer<GetAllStorageItemsOnInstanceConsumer>();
                x.AddConsumer<GetAllPlacesOfStorageItemOnInstanceConsumer>();
                
                x.AddConsumer<UpdateStorageItemPurchasePriceConsumer>();
                x.AddConsumer<UpdateStorageItemRetailPriceConsumer>();
                x.AddConsumer<UpdateStorageItemHiddenCommentConsumer>();
                x.AddConsumer<UpdateStorageItemConditionConsumer>();
                x.AddConsumer<UpdateStorageItemCommentConsumer>();
                x.AddConsumer<UpdateStorageItemArticleConsumer>();
                x.AddConsumer<CreateStorageItemConsumer>();
                x.AddConsumer<GetAllStorageItemsConsumer>();
                x.AddConsumer<GetStorageItemByIdConsumer>();
                x.AddConsumer<GetAllStorageItemConditionsConsumer>();
                
                x.AddConsumer<GetAllArticlesCategoriesConsumer>();
                x.AddConsumer<CreateCategoryConsumer>();
                x.AddConsumer<UpdateCategoryParentCategoryIdConsumer>();
                x.AddConsumer<UpdateCategoryNameConsumer>();
                
                x.AddConsumer<GetArticleByIdConsumer>();
                x.AddConsumer<GetAllArticlesConsumer>();
                x.AddConsumer<GetAllArticlesModelsConsumer>();
                x.AddConsumer<GetAllArticlesBrandsConsumer>();
                x.AddConsumer<CreateArticleConsumer>();
                x.AddConsumer<UpdateArticleModelConsumer>();
                x.AddConsumer<UpdateArticleInfoConsumer>();
                x.AddConsumer<UpdateArticleCategoryConsumer>();
                x.AddConsumer<UpdateArticleBrandConsumer>();
                
                x.AddConsumer<CheckExistArticleConsumer>();
                x.AddConsumer<CheckExistStorageItemConsumer>();
                
                x.AddRequestClient<IGetClientByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetClientByIdRequestContract"));
                x.AddRequestClient<IGetUnitByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetUnitByIdRequestContract"));
                x.AddRequestClient<IGetUnitStringByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<IGetClientStringByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<IGetUserByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetUserByIdRequestContract"));
                x.AddRequestClient<ICreateComingCashOperationRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateComingCashOperationRequestContract"));
                x.AddRequestClient<IGetImagesOfStorageItemRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetImagesOfStorageItemRequestContract"));
            });
            services.AddMassTransitHostedService();
        }
    }
}
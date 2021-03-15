using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
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
using Nano35.Storage.Processor.UseCases.GetAllCancellations;
using Nano35.Storage.Processor.UseCases.GetAllComings;
using Nano35.Storage.Processor.UseCases.GetAllMoves;
using Nano35.Storage.Processor.UseCases.GetAllPlacesOnStorage;
using Nano35.Storage.Processor.UseCases.GetAllSells;
using Nano35.Storage.Processor.UseCases.GetAllStorageItemConditions;
using Nano35.Storage.Processor.UseCases.GetAllStorageItems;
using Nano35.Storage.Processor.UseCases.GetAllWarehousesOfItem;
using Nano35.Storage.Processor.UseCases.GetAllWarehousesOfUnit;
using Nano35.Storage.Processor.UseCases.GetArticleById;
using Nano35.Storage.Processor.UseCases.GetCancellationDetailsById;
using Nano35.Storage.Processor.UseCases.GetComingDetailsById;
using Nano35.Storage.Processor.UseCases.GetMoveDetailsById;
using Nano35.Storage.Processor.UseCases.GetSelleDetailsById;
using Nano35.Storage.Processor.UseCases.GetStorageItemById;
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
                    
                    cfg.ReceiveEndpoint("IGetAllArticlesRequestContract", e =>
                    {
                        e.Consumer<GetAllArticlesConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("ICreateCancellationRequestContract", e =>
                    {
                        e.Consumer<CreateCancellationConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("ICreateSelleRequestContract", e =>
                    {
                        e.Consumer<CreateSelleConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("ICreateComingRequestContract", e =>
                    {
                        e.Consumer<CreateComingConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("ICreateMoveRequestContract", e =>
                    {
                        e.Consumer<CreateMoveConsumer>(provider);
                    });
                    
                    cfg.ReceiveEndpoint("ICreateCategoryRequestContract", e =>
                    {
                        e.Consumer<CreateCategoryConsumer>(provider);
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

                    cfg.ReceiveEndpoint("IGetAllComingsRequestContract", e =>
                    {
                        e.Consumer<GetAllComingsConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IGetComingDetailsByIdRequestContract", e =>
                    {
                        e.Consumer<GetComingDetailsByIdConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IGetAllCancellationsRequestContract", e =>
                    {
                        e.Consumer<GetAllCancellationsConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IGetAllMovesRequestContract", e =>
                    {
                        e.Consumer<GetAllMovesConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IGetAllSellsRequestContract", e =>
                    {
                        e.Consumer<GetAllSellsConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IGetAllWarehousesOfItemRequestContract", e =>
                    {
                        e.Consumer<GetAllWarehousesOfItemConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IGetAllWarehousesOfUnitRequestContract", e =>
                    {
                        e.Consumer<GetAllWarehousesOfUnitConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IGetCancellationDetailsByIdRequestContract", e =>
                    {
                        e.Consumer<GetCancellationDetailsByIdConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IGetMoveDetailsByIdRequestContract", e =>
                    {
                        e.Consumer<GetMoveDetailsByIdConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IGetSelleDetailsByIdRequestContract", e =>
                    {
                        e.Consumer<GetSelleDetailsByIdConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IUpdateArticleBrandRequestContract", e =>
                    {
                        e.Consumer<UpdateArticleBrandConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IUpdateArticleCategoryRequestContract", e =>
                    {
                        e.Consumer<UpdateArticleCategoryConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IUpdateArticleInfoRequestContract", e =>
                    {
                        e.Consumer<UpdateArticleInfoConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IUpdateArticleModelRequestContract", e =>
                    {
                        e.Consumer<UpdateArticleModelConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IUpdateCategoryNameRequestContract", e =>
                    {
                        e.Consumer<UpdateCategoryNameConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IUpdateCategoryParentCategoryIdRequestContract", e =>
                    {
                        e.Consumer<UpdateParentCategoryConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IUpdateStorageItemArticleRequestContract", e =>
                    {
                        e.Consumer<UpdateStorageItemArticleConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IUpdateStorageItemCommentRequestContract", e =>
                    {
                        e.Consumer<UpdateStorageItemCommentConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IUpdateStorageItemConditionRequestContract", e =>
                    {
                        e.Consumer<UpdateStorageItemConditionConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IUpdateStorageItemHiddenCommentRequestContract", e =>
                    {
                        e.Consumer<UpdateStorageItemHiddenCommentConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IUpdateStorageItemPurchasePriceRequestContract", e =>
                    {
                        e.Consumer<UpdateStorageItemPurchasePriceConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IUpdateStorageItemRetailPriceRequestContract", e =>
                    {
                        e.Consumer<UpdateStorageItemRetailPriceConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("IGetAllPlacesOnStorageContract", e =>
                    {
                        e.Consumer<GetAllPlacesOnStorageConsumer>(provider);
                    });
                    
                }));
                x.AddConsumer<GetAllPlacesOnStorageConsumer>();
                x.AddConsumer<UpdateStorageItemRetailPriceConsumer>();
                x.AddConsumer<UpdateStorageItemPurchasePriceConsumer>();
                x.AddConsumer<UpdateStorageItemHiddenCommentConsumer>();
                x.AddConsumer<UpdateStorageItemConditionConsumer>();
                x.AddConsumer<UpdateStorageItemCommentConsumer>();
                x.AddConsumer<UpdateStorageItemArticleConsumer>();
                x.AddConsumer<UpdateParentCategoryConsumer>();
                x.AddConsumer<UpdateCategoryNameConsumer>();
                x.AddConsumer<UpdateArticleModelConsumer>();
                x.AddConsumer<UpdateArticleInfoConsumer>();
                x.AddConsumer<UpdateArticleCategoryConsumer>();
                x.AddConsumer<UpdateArticleBrandConsumer>();
                x.AddConsumer<GetSelleDetailsByIdConsumer>();
                x.AddConsumer<GetMoveDetailsByIdConsumer>();
                x.AddConsumer<GetCancellationDetailsByIdConsumer>();
                x.AddConsumer<GetAllWarehousesOfUnitConsumer>();
                x.AddConsumer<GetAllWarehousesOfItemConsumer>();
                x.AddConsumer<GetAllSellsConsumer>();
                x.AddConsumer<GetAllMovesConsumer>();
                x.AddConsumer<GetAllCancellationsConsumer>();
                x.AddConsumer<CreateArticleConsumer>();
                x.AddConsumer<CreateSelleConsumer>();
                x.AddConsumer<GetAllArticlesBrandsConsumer>();
                x.AddConsumer<CreateStorageItemConsumer>();
                x.AddConsumer<GetAllArticlesConsumer>();
                x.AddConsumer<GetArticleByIdConsumer>();
                x.AddConsumer<GetAllStorageItemsConsumer>();
                x.AddConsumer<GetStorageItemByIdConsumer>();
                x.AddConsumer<GetAllArticlesCategoriesConsumer>();
                x.AddConsumer<GetAllArticlesModelsConsumer>();
                x.AddConsumer<GetAllStorageItemConditionsConsumer>();
                x.AddConsumer<CreateCancellationConsumer>();
                x.AddConsumer<CreateCategoryConsumer>();
                x.AddConsumer<CreateComingConsumer>();
                x.AddConsumer<CreateMoveConsumer>();
                x.AddConsumer<GetAllComingsConsumer>();
                x.AddConsumer<GetComingDetailsByIdConsumer>();
                
                x.AddRequestClient<IGetClientByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetClientByIdRequestContract"));
                x.AddRequestClient<IGetUnitByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetUnitByIdRequestContract"));
                x.AddRequestClient<IGetUnitStringByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<IGetClientStringByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/ICreateClientRequestContract"));
                x.AddRequestClient<IGetUserByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetUserByIdRequestContract"));
            });
            services.AddMassTransitHostedService();
        }
    }
}
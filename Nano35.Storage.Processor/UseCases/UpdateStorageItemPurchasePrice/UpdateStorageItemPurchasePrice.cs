using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemPurchasePrice
{
    public class UpdateStorageItemPurchasePriceConsumer : 
        IConsumer<IUpdateStorageItemPurchasePriceRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public UpdateStorageItemPurchasePriceConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateStorageItemPurchasePriceRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedUpdateStorageItemPurchasePriceRequest>) _services
                .GetService(typeof(ILogger<LoggedUpdateStorageItemPurchasePriceRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedUpdateStorageItemPurchasePriceRequest(logger,
                    new ValidatedUpdateStorageItemPurchasePriceRequest(
                        new UpdateStorageItemPurchasePriceRequest(dbContext))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create PurchasePrice request
            switch (result)
            {
                case IUpdateStorageItemPurchasePriceSuccessResultContract:
                    await context.RespondAsync<IUpdateStorageItemPurchasePriceSuccessResultContract>(result);
                    break;
                case IUpdateStorageItemPurchasePriceErrorResultContract:
                    await context.RespondAsync<IUpdateStorageItemPurchasePriceErrorResultContract>(result);
                    break;
            }
        }
    }
}
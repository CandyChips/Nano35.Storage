using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemRetailPrice
{
    public class UpdateStorageItemRetailPriceConsumer : 
        IConsumer<IUpdateStorageItemRetailPriceRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public UpdateStorageItemRetailPriceConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateStorageItemRetailPriceRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedUpdateStorageItemRetailPriceRequest>) _services
                .GetService(typeof(ILogger<LoggedUpdateStorageItemRetailPriceRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedUpdateStorageItemRetailPriceRequest(logger,
                    new ValidatedUpdateStorageItemRetailPriceRequest(
                        new UpdateStorageItemRetailPriceRequest(dbContext))).Ask(message, context.CancellationToken);
            
            // Check response of create RetailPrice request
            switch (result)
            {
                case IUpdateStorageItemRetailPriceSuccessResultContract:
                    await context.RespondAsync<IUpdateStorageItemRetailPriceSuccessResultContract>(result);
                    break;
                case IUpdateStorageItemRetailPriceErrorResultContract:
                    await context.RespondAsync<IUpdateStorageItemRetailPriceErrorResultContract>(result);
                    break;
            }
        }
    }
}
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
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IUpdateStorageItemPurchasePriceRequestContract>) _services.GetService(typeof(ILogger<IUpdateStorageItemPurchasePriceRequestContract>));
            var message = context.Message;
            var result = await new LoggedPipeNode<IUpdateStorageItemPurchasePriceRequestContract, IUpdateStorageItemPurchasePriceResultContract>(logger,
                new ValidatedUpdateStorageItemPurchasePriceRequest(
                    new TransactedPipeNode<IUpdateStorageItemPurchasePriceRequestContract, IUpdateStorageItemPurchasePriceResultContract>(dbContext, 
                        new UpdateStorageItemPurchasePriceRequest(dbContext)))).Ask(message, context.CancellationToken);
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
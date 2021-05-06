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
            var result =
                await new LoggedUseCasePipeNode<IUpdateStorageItemPurchasePriceRequestContract, IUpdateStorageItemPurchasePriceResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateStorageItemPurchasePriceRequestContract>)) as
                            ILogger<IUpdateStorageItemPurchasePriceRequestContract>,
                        new TransactedUseCasePipeNode<IUpdateStorageItemPurchasePriceRequestContract,
                            IUpdateStorageItemPurchasePriceResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new UpdateStorageItemPurchasePriceRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
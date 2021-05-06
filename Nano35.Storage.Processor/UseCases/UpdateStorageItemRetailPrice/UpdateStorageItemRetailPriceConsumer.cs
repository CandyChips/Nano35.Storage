using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;
using Nano35.Storage.Processor.UseCases.UpdateStorageItemRetailPrice;

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
            var result =
                await new LoggedUseCasePipeNode<IUpdateStorageItemRetailPriceRequestContract, IUpdateStorageItemRetailPriceResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateStorageItemRetailPriceRequestContract>)) as
                            ILogger<IUpdateStorageItemRetailPriceRequestContract>,
                        new TransactedUseCasePipeNode<IUpdateStorageItemRetailPriceRequestContract,
                            IUpdateStorageItemRetailPriceResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new UpdateStorageItemRetailPriceRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
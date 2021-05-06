using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.PresentationGetAllStorageItems
{
    public class PresentationGetAllStorageItemsConsumer : 
        IConsumer<IPresentationGetAllStorageItemsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public PresentationGetAllStorageItemsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IPresentationGetAllStorageItemsRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IPresentationGetAllStorageItemsRequestContract, IPresentationGetAllStorageItemsResultContract>(
                        _services.GetService(typeof(ILogger<IPresentationGetAllStorageItemsRequestContract>)) as
                            ILogger<IPresentationGetAllStorageItemsRequestContract>,
                        new PresentationGetAllStorageItemsRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                                _services.GetService(typeof(IBus)) as IBus))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
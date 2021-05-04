using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItems
{
    public class GetAllStorageItemsConsumer : 
        IConsumer<IGetAllStorageItemsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllStorageItemsConsumer(IServiceProvider services) { _services = services; }
        
        public async Task Consume(
            ConsumeContext<IGetAllStorageItemsRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllStorageItemsRequestContract, IGetAllStorageItemsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllStorageItemsRequestContract>)) as ILogger<IGetAllStorageItemsRequestContract>,
                        new GetAllStorageItemsRequest(                            
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
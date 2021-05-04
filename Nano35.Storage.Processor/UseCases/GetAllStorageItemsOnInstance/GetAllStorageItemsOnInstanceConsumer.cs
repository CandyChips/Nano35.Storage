using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemsOnInstance
{
    public class GetAllStorageItemsOnInstanceConsumer : 
        IConsumer<IGetAllStorageItemsOnInstanceContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllStorageItemsOnInstanceConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllStorageItemsOnInstanceContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllStorageItemsOnInstanceContract, IGetAllStorageItemsOnInstanceResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllStorageItemsOnInstanceContract>)) as ILogger<IGetAllStorageItemsOnInstanceContract>,
                        new GetAllStorageItemsOnInstanceRequest(                            
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
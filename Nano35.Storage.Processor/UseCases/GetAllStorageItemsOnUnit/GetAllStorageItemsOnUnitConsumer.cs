using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemsOnUnit
{
    public class GetAllStorageItemsOnUnitConsumer : 
        IConsumer<IGetAllStorageItemsOnUnitContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllStorageItemsOnUnitConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllStorageItemsOnUnitContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllStorageItemsOnUnitContract, IGetAllStorageItemsOnUnitResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllStorageItemsOnUnitContract>)) as ILogger<IGetAllStorageItemsOnUnitContract>,
                        new GetAllStorageItemsOnUnitRequest(                            
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllPlacesOfStorageItemOnInstance
{
    public class GetAllPlacesOfStorageItemOnInstanceConsumer : 
        IConsumer<IGetAllPlacesOfStorageItemOnInstanceContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllPlacesOfStorageItemOnInstanceConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllPlacesOfStorageItemOnInstanceContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllPlacesOfStorageItemOnInstanceContract, IGetAllPlacesOfStorageItemOnInstanceResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllPlacesOfStorageItemOnInstanceContract>)) as ILogger<IGetAllPlacesOfStorageItemOnInstanceContract>,
                        new GetAllPlacesOfStorageItemOnInstanceRequest(                            
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
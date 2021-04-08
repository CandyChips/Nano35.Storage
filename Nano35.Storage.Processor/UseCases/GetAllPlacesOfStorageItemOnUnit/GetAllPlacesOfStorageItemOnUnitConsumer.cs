using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllPlacesOfStorageItemOnUnit
{
    public class GetAllPlacesOfStorageItemOnUnitConsumer : 
        IConsumer<IGetAllPlacesOfStorageItemOnUnitRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllPlacesOfStorageItemOnUnitConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllPlacesOfStorageItemOnUnitRequestContract> context)
        {
            var result = await new LoggedPipeNode<IGetAllPlacesOfStorageItemOnUnitRequestContract, IGetAllPlacesOfStorageItemOnUnitResultContract>(
                _services.GetService(typeof(ILogger<IGetAllPlacesOfStorageItemOnUnitRequestContract>)) as ILogger<IGetAllPlacesOfStorageItemOnUnitRequestContract>,
                new GetAllPlacesOfStorageItemOnUnitRequest(
                    _services.GetService(typeof(ApplicationContext)) as ApplicationContext, 
                    _services.GetService(typeof(IBus)) as IBus)).Ask(context.Message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllPlacesOfStorageItemOnUnitSuccessResultContract:
                    await context.RespondAsync<IGetAllPlacesOfStorageItemOnUnitSuccessResultContract>(result);
                    break;
                case IGetAllPlacesOfStorageItemOnUnitErrorResultContract:
                    await context.RespondAsync<IGetAllPlacesOfStorageItemOnUnitErrorResultContract>(result);
                    break;
            }
        }
    }
}
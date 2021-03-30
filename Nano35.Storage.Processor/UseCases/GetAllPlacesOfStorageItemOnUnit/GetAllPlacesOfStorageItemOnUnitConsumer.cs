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
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllPlacesOfStorageItemOnUnitRequest>) _services.GetService(typeof(ILogger<LoggedGetAllPlacesOfStorageItemOnUnitRequest>));

            var message = context.Message;

            var result = await new LoggedGetAllPlacesOfStorageItemOnUnitRequest(logger,
                        new ValidatedGetAllPlacesOfStorageItemOnUnitRequest(
                            new GetAllPlacesOfStorageItemOnUnitRequest(dbContext, bus))).Ask(message, context.CancellationToken);
            
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
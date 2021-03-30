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
            // Setup configuration of pipeline
            var bus = (IBus) _services.GetService(typeof(IBus));
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetAllStorageItemsOnUnitRequest>) _services
                .GetService(typeof(ILogger<LoggedGetAllStorageItemsOnUnitRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedGetAllStorageItemsOnUnitRequest(logger,
                    new ValidatedGetAllStorageItemsOnUnitRequest(
                        new GetAllStorageItemsOnUnitRequest(dbContext,bus))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IGetAllStorageItemsOnUnitSuccessResultContract:
                    await context.RespondAsync<IGetAllStorageItemsOnUnitSuccessResultContract>(result);
                    break;
                case IGetAllStorageItemsOnUnitErrorResultContract:
                    await context.RespondAsync<IGetAllStorageItemsOnUnitErrorResultContract>(result);
                    break;
            }
        }
    }
}
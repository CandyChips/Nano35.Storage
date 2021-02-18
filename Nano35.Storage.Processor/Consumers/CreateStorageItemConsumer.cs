using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests.CreateStorageItem;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Consumers
{
    public class CreateStorageItemConsumer : 
        IConsumer<ICreateStorageItemRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public CreateStorageItemConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<ICreateStorageItemRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedCreateStorageItemRequest>) _services.GetService(typeof(ILogger<LoggedCreateStorageItemRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedCreateStorageItemRequest(logger,
                    new ValidatedCreateStorageItemRequest(
                        new TransactedCreateStorageItemRequest(dbContext,
                            new CreateStorageItemRequest(dbContext)))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case ICreateStorageItemSuccessResultContract:
                    await context.RespondAsync<ICreateStorageItemSuccessResultContract>(result);
                    break;
                case ICreateStorageItemErrorResultContract:
                    await context.RespondAsync<ICreateStorageItemErrorResultContract>(result);
                    break;
            }
        }
    }
}
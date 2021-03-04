using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemCondition
{
    public class UpdateStorageItemConditionConsumer : 
        IConsumer<IUpdateStorageItemConditionRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public UpdateStorageItemConditionConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateStorageItemConditionRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedUpdateStorageItemConditionRequest>) _services
                .GetService(typeof(ILogger<LoggedUpdateStorageItemConditionRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedUpdateStorageItemConditionRequest(logger,
                    new ValidatedUpdateStorageItemConditionRequest(
                        new UpdateStorageItemConditionRequest(dbContext))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create Condition request
            switch (result)
            {
                case IUpdateStorageItemConditionSuccessResultContract:
                    await context.RespondAsync<IUpdateStorageItemConditionSuccessResultContract>(result);
                    break;
                case IUpdateStorageItemConditionErrorResultContract:
                    await context.RespondAsync<IUpdateStorageItemConditionErrorResultContract>(result);
                    break;
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests.GetAllArticle;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Consumers
{
    public class GetAllStorageItemConditionsConsumer : 
        IConsumer<IGetAllStorageItemConditionsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllStorageItemConditionsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<IGetAllStorageItemConditionsRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<GetAllStorageItemConditionsLogger>) _services.GetService(typeof(ILogger<GetAllStorageItemConditionsLogger>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new GetAllStorageItemConditionsLogger(logger,
                    new GetAllStorageItemConditionsValidator(
                        new GetAllStorageItemConditionsRequest(dbContext))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IGetAllStorageItemConditionsSuccessResultContract:
                    await context.RespondAsync<IGetAllStorageItemConditionsSuccessResultContract>(result);
                    break;
                case IGetAllStorageItemConditionsErrorResultContract:
                    await context.RespondAsync<IGetAllStorageItemConditionsErrorResultContract>(result);
                    break;
            }
        }
    }
}
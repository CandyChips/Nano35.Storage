using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests.GetAllArticleBrands;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Consumers
{
    public class GetStorageItemByIdConsumer : 
        IConsumer<IGetStorageItemByIdRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetStorageItemByIdConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetStorageItemByIdRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<GetStorageItemByIdLogger>) _services.GetService(typeof(ILogger<GetStorageItemByIdLogger>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new GetStorageItemByIdLogger(logger,
                    new GetStorageItemByIdValidator(
                        new GetStorageItemByIdRequest(dbContext))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IGetStorageItemByIdSuccessResultContract:
                    await context.RespondAsync<IGetStorageItemByIdSuccessResultContract>(result);
                    break;
                case IGetStorageItemByIdErrorResultContract:
                    await context.RespondAsync<IGetStorageItemByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}
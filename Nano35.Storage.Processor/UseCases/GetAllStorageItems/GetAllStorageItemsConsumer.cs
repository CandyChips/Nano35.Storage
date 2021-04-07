using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItems
{
    public class GetAllStorageItemsConsumer : 
        IConsumer<IGetAllStorageItemsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllStorageItemsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllStorageItemsRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IGetAllStorageItemsRequestContract>) _services
                .GetService(typeof(ILogger<IGetAllStorageItemsRequestContract>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedPipeNode<IGetAllStorageItemsRequestContract, IGetAllStorageItemsResultContract>(logger,
                    new ValidatedGetAllStorageItemsRequest(
                        new GetAllStorageItemsRequest(dbContext))).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IGetAllStorageItemsSuccessResultContract:
                    await context.RespondAsync<IGetAllStorageItemsSuccessResultContract>(result);
                    break;
                case IGetAllStorageItemsErrorResultContract:
                    await context.RespondAsync<IGetAllStorageItemsErrorResultContract>(result);
                    break;
            }
        }
    }
}
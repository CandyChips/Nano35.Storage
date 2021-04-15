using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.PresentationGetAllStorageItems
{
    public class PresentationGetAllStorageItemsConsumer : 
        IConsumer<IPresentationGetAllStorageItemsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public PresentationGetAllStorageItemsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IPresentationGetAllStorageItemsRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IPresentationGetAllStorageItemsRequestContract>) _services.GetService(typeof(ILogger<IPresentationGetAllStorageItemsRequestContract>));
            var message = context.Message;
            var result = await new LoggedPipeNode<IPresentationGetAllStorageItemsRequestContract, IPresentationGetAllStorageItemsResultContract>(logger,
                new PresentationGetAllStorageItemsRequest(dbContext)).Ask(message, context.CancellationToken);
            switch (result)
            {
                case IPresentationGetAllStorageItemsSuccessResultContract:
                    await context.RespondAsync<IPresentationGetAllStorageItemsSuccessResultContract>(result);
                    break;
                case IPresentationGetAllStorageItemsErrorResultContract:
                    await context.RespondAsync<IPresentationGetAllStorageItemsErrorResultContract>(result);
                    break;
            }
        }
    }
}
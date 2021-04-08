using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateStorageItem
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
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<ICreateStorageItemRequestContract>) _services.GetService(typeof(ILogger<ICreateStorageItemRequestContract>));
            var message = context.Message;
            var result = await new LoggedPipeNode<ICreateStorageItemRequestContract, ICreateStorageItemResultContract>(logger,
                new TransactedPipeNode<ICreateStorageItemRequestContract, ICreateStorageItemResultContract>(dbContext,
                    new CreateStorageItemRequest(dbContext))).Ask(message, context.CancellationToken);
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
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
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IUpdateStorageItemConditionRequestContract>) _services.GetService(typeof(ILogger<IUpdateStorageItemConditionRequestContract>));
            var message = context.Message;
            var result =
                await new LoggedPipeNode<IUpdateStorageItemConditionRequestContract, IUpdateStorageItemConditionResultContract>(logger,
                    new TransactedPipeNode<IUpdateStorageItemConditionRequestContract, IUpdateStorageItemConditionResultContract>(dbContext, 
                        new UpdateStorageItemConditionRequest(dbContext))).Ask(message, context.CancellationToken);
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
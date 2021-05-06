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
            var result =
                await new LoggedUseCasePipeNode<IUpdateStorageItemConditionRequestContract, IUpdateStorageItemConditionResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateStorageItemConditionRequestContract>)) as
                            ILogger<IUpdateStorageItemConditionRequestContract>,
                        new TransactedUseCasePipeNode<IUpdateStorageItemConditionRequestContract,
                            IUpdateStorageItemConditionResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new UpdateStorageItemConditionRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
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
        public CreateStorageItemConsumer(IServiceProvider services) { _services = services; }
        public async Task Consume(ConsumeContext<ICreateStorageItemRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<ICreateStorageItemRequestContract, ICreateStorageItemResultContract>(
                        _services.GetService(typeof(ILogger<ICreateStorageItemRequestContract>)) as
                            ILogger<ICreateStorageItemRequestContract>,
                        new TransactedUseCasePipeNode<ICreateStorageItemRequestContract,
                            ICreateStorageItemResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new CreateStorageItemRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
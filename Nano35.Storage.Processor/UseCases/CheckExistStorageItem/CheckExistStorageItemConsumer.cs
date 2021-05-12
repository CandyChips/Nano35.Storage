using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;
using Nano35.Storage.Processor.UseCases.CreateArticle;

namespace Nano35.Storage.Processor.UseCases.CheckExistStorageItem
{
    public class CheckExistStorageItemConsumer : IConsumer<ICheckExistStorageItemRequestContract>
    {
        private readonly IServiceProvider _services;
        public CheckExistStorageItemConsumer(IServiceProvider services) { _services = services; }
        public async Task Consume(ConsumeContext<ICheckExistStorageItemRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<ICheckExistStorageItemRequestContract, ICheckExistStorageItemResultContract>(
                    _services.GetService(typeof(ILogger<ICheckExistStorageItemRequestContract>)) as ILogger<ICheckExistStorageItemRequestContract>,
                    new CheckExistStorageItemRequest(_services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemArticle
{
    public class UpdateStorageItemArticleConsumer : 
        IConsumer<IUpdateStorageItemArticleRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public UpdateStorageItemArticleConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateStorageItemArticleRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IUpdateStorageItemArticleRequestContract, IUpdateStorageItemArticleResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateStorageItemArticleRequestContract>)) as
                            ILogger<IUpdateStorageItemArticleRequestContract>,
                        new TransactedUseCasePipeNode<IUpdateStorageItemArticleRequestContract,
                            IUpdateStorageItemArticleResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new UpdateStorageItemArticleRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
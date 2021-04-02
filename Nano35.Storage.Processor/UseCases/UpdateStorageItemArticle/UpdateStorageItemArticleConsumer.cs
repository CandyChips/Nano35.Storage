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
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IUpdateStorageItemArticleRequestContract>) _services.GetService(typeof(ILogger<IUpdateStorageItemArticleRequestContract>));
            var message = context.Message;
            var result =
                await new LoggedPipeNode<IUpdateStorageItemArticleRequestContract, IUpdateStorageItemArticleResultContract>(logger,
                    new ValidatedUpdateStorageItemArticleRequest(
                        new TransactedPipeNode<IUpdateStorageItemArticleRequestContract, IUpdateStorageItemArticleResultContract>(dbContext, 
                            new UpdateStorageItemArticleRequest(dbContext)))).Ask(message, context.CancellationToken);
            switch (result)
            {
                case IUpdateStorageItemArticleSuccessResultContract:
                    await context.RespondAsync<IUpdateStorageItemArticleSuccessResultContract>(result);
                    break;
                case IUpdateStorageItemArticleErrorResultContract:
                    await context.RespondAsync<IUpdateStorageItemArticleErrorResultContract>(result);
                    break;
            }
        }
    }
}
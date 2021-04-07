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
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedUpdateStorageItemArticleRequest>) _services
                .GetService(typeof(ILogger<LoggedUpdateStorageItemArticleRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedUpdateStorageItemArticleRequest(logger,
                    new ValidatedUpdateStorageItemArticleRequest(
                        new UpdateStorageItemArticleRequest(dbContext))).Ask(message, context.CancellationToken);
            
            // Check response of create article request
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
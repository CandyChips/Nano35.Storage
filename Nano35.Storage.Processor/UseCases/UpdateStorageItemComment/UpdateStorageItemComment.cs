using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemComment
{
    public class UpdateStorageItemCommentConsumer : 
        IConsumer<IUpdateStorageItemCommentRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public UpdateStorageItemCommentConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateStorageItemCommentRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedUpdateStorageItemCommentRequest>) _services
                .GetService(typeof(ILogger<LoggedUpdateStorageItemCommentRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedUpdateStorageItemCommentRequest(logger,
                    new ValidatedUpdateStorageItemCommentRequest(
                        new UpdateStorageItemCommentRequest(dbContext))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create Comment request
            switch (result)
            {
                case IUpdateStorageItemCommentSuccessResultContract:
                    await context.RespondAsync<IUpdateStorageItemCommentSuccessResultContract>(result);
                    break;
                case IUpdateStorageItemCommentErrorResultContract:
                    await context.RespondAsync<IUpdateStorageItemCommentErrorResultContract>(result);
                    break;
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemHiddenComment
{
    public class UpdateStorageItemHiddenCommentConsumer : 
        IConsumer<IUpdateStorageItemHiddenCommentRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public UpdateStorageItemHiddenCommentConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateStorageItemHiddenCommentRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedUpdateStorageItemHiddenCommentRequest>) _services
                .GetService(typeof(ILogger<LoggedUpdateStorageItemHiddenCommentRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedUpdateStorageItemHiddenCommentRequest(logger,
                    new ValidatedUpdateStorageItemHiddenCommentRequest(
                        new UpdateStorageItemHiddenCommentRequest(dbContext))).Ask(message, context.CancellationToken);
            
            // Check response of create HiddenComment request
            switch (result)
            {
                case IUpdateStorageItemHiddenCommentSuccessResultContract:
                    await context.RespondAsync<IUpdateStorageItemHiddenCommentSuccessResultContract>(result);
                    break;
                case IUpdateStorageItemHiddenCommentErrorResultContract:
                    await context.RespondAsync<IUpdateStorageItemHiddenCommentErrorResultContract>(result);
                    break;
            }
        }
    }
}
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
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IUpdateStorageItemCommentRequestContract>) _services.GetService(typeof(ILogger<IUpdateStorageItemCommentRequestContract>));
            var message = context.Message;
            var result =
                await new LoggedPipeNode<IUpdateStorageItemCommentRequestContract, IUpdateStorageItemCommentResultContract>(logger,
                    new TransactedPipeNode<IUpdateStorageItemCommentRequestContract, IUpdateStorageItemCommentResultContract>(dbContext, 
                        new UpdateStorageItemCommentRequest(dbContext))).Ask(message, context.CancellationToken);
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
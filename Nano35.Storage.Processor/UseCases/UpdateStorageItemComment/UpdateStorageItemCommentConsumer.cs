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
            var result =
                await new LoggedUseCasePipeNode<IUpdateStorageItemCommentRequestContract, IUpdateStorageItemCommentResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateStorageItemCommentRequestContract>)) as
                            ILogger<IUpdateStorageItemCommentRequestContract>,
                        new TransactedUseCasePipeNode<IUpdateStorageItemCommentRequestContract,
                            IUpdateStorageItemCommentResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new UpdateStorageItemCommentRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
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
            var result =
                await new LoggedUseCasePipeNode<IUpdateStorageItemHiddenCommentRequestContract, IUpdateStorageItemHiddenCommentResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateStorageItemHiddenCommentRequestContract>)) as
                            ILogger<IUpdateStorageItemHiddenCommentRequestContract>,
                        new TransactedUseCasePipeNode<IUpdateStorageItemHiddenCommentRequestContract,
                            IUpdateStorageItemHiddenCommentResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new UpdateStorageItemHiddenCommentRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
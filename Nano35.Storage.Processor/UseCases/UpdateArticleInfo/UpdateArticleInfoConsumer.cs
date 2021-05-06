using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleInfo
{
    public class UpdateArticleInfoConsumer : 
        IConsumer<IUpdateArticleInfoRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public UpdateArticleInfoConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateArticleInfoRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IUpdateArticleInfoRequestContract, IUpdateArticleInfoResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateArticleInfoRequestContract>)) as
                            ILogger<IUpdateArticleInfoRequestContract>,
                        new TransactedUseCasePipeNode<IUpdateArticleInfoRequestContract,
                            IUpdateArticleInfoResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new UpdateArticleInfoRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
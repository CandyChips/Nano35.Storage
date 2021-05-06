using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleModel
{
    public class UpdateArticleModelConsumer : 
        IConsumer<IUpdateArticleModelRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public UpdateArticleModelConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateArticleModelRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IUpdateArticleModelRequestContract, IUpdateArticleModelResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateArticleModelRequestContract>)) as
                            ILogger<IUpdateArticleModelRequestContract>,
                        new TransactedUseCasePipeNode<IUpdateArticleModelRequestContract,
                            IUpdateArticleModelResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new UpdateArticleModelRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
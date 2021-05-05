using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;
using Nano35.Storage.Processor.UseCases.CreateArticle;

namespace Nano35.Storage.Processor.UseCases.CheckExistArticle
{
    public class CheckExistArticleConsumer : 
        IConsumer<ICheckExistArticleRequestContract>
    {
        private readonly IServiceProvider _services;
        public CheckExistArticleConsumer(IServiceProvider services) { _services = services; }
        
        public async Task Consume(
            ConsumeContext<ICheckExistArticleRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<ICheckExistArticleRequestContract, ICheckExistArticleResultContract>(
                        _services.GetService(typeof(ILogger<ICheckExistArticleRequestContract>)) as
                            ILogger<ICheckExistArticleRequestContract>,
                        new TransactedUseCasePipeNode<ICheckExistArticleRequestContract,
                            ICheckExistArticleResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new CheckExistArticleRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateArticle
{
    public class CreateArticleConsumer : 
        IConsumer<ICreateArticleRequestContract>
    {
        private readonly IServiceProvider _services;
        public CreateArticleConsumer(IServiceProvider services) { _services = services; }
        
        public async Task Consume(
            ConsumeContext<ICreateArticleRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<ICreateArticleRequestContract, ICreateArticleResultContract>(
                        _services.GetService(typeof(ILogger<ICreateArticleRequestContract>)) as
                            ILogger<ICreateArticleRequestContract>,
                        new TransactedUseCasePipeNode<ICreateArticleRequestContract,
                            ICreateArticleResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new CreateArticleRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
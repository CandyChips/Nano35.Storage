using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.PresentationGetAllArticles
{
    public class PresentationGetAllArticlesConsumer : 
        IConsumer<IPresentationGetAllArticlesRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public PresentationGetAllArticlesConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IPresentationGetAllArticlesRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IPresentationGetAllArticlesRequestContract, IPresentationGetAllArticlesResultContract>(
                        _services.GetService(typeof(ILogger<IPresentationGetAllArticlesRequestContract>)) as
                            ILogger<IPresentationGetAllArticlesRequestContract>,
                            new PresentationGetAllArticlesRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllArticles
{
    public class GetAllArticlesConsumer : 
        IConsumer<IGetAllArticlesRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllArticlesConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllArticlesRequestContract> context)
        {
                var result =
                    await new LoggedUseCasePipeNode<IGetAllArticlesRequestContract, IGetAllArticlesResultContract>(
                            _services.GetService(typeof(ILogger<IGetAllArticlesRequestContract>)) as ILogger<IGetAllArticlesRequestContract>,
                            new GetAllArticlesRequest(                            
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                        .Ask(context.Message, context.CancellationToken);
                await context.RespondAsync(result);
            }
    }
}
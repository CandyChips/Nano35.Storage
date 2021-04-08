using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllArticleModels
{
    public class GetAllArticlesModelsConsumer : 
        IConsumer<IGetAllArticlesModelsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllArticlesModelsConsumer(IServiceProvider services) { _services = services; }
        
        public async Task Consume(
            ConsumeContext<IGetAllArticlesModelsRequestContract> context)
        {
            var result = await new LoggedPipeNode<IGetAllArticlesModelsRequestContract, IGetAllArticlesModelsResultContract>(
                _services.GetService(typeof(ILogger<IGetAllArticlesModelsRequestContract>)) as ILogger<IGetAllArticlesModelsRequestContract>,
                new GetAllArticlesModelsRequest(
                    _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                .Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IGetAllArticlesModelsSuccessResultContract:
                    await context.RespondAsync<IGetAllArticlesModelsSuccessResultContract>(result);
                    break;
                case IGetAllArticlesModelsErrorResultContract:
                    await context.RespondAsync<IGetAllArticlesModelsErrorResultContract>(result);
                    break;
            }
        }
    }
}
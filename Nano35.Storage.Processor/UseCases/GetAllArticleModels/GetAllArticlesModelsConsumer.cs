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
        
        public GetAllArticlesModelsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllArticlesModelsRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IGetAllArticlesModelsRequestContract>) _services.GetService(typeof(ILogger<IGetAllArticlesModelsRequestContract>));
            var message = context.Message;
            var result = await new LoggedPipeNode<IGetAllArticlesModelsRequestContract, IGetAllArticlesModelsResultContract>(logger,
                new GetAllArticlesModelsRequest(dbContext)).Ask(message, context.CancellationToken);
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
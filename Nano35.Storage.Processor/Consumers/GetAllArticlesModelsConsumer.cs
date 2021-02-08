using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests.GetAllArticleModels;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Consumers
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
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<GetAllArticlesModelsLogger>) _services.GetService(typeof(ILogger<GetAllArticlesModelsLogger>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new GetAllArticlesModelsLogger(logger,
                    new GetAllArticlesModelsValidator(
                        new GetAllArticlesModelsRequest(dbContext))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
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
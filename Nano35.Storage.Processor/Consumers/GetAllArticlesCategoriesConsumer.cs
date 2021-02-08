using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests.GetAllArticleCategories;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Consumers
{
    public class GetAllArticlesCategoriesConsumer : 
        IConsumer<IGetAllArticlesCategoriesRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllArticlesCategoriesConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllArticlesCategoriesRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<GetAllArticlesCategoriesLogger>) _services.GetService(typeof(ILogger<GetAllArticlesCategoriesLogger>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new GetAllArticlesCategoriesLogger(logger,
                    new GetAllArticlesCategoriesValidator(
                        new GetAllArticlesCategoriesRequest(dbContext))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IGetAllArticlesCategoriesSuccessResultContract:
                    await context.RespondAsync<IGetAllArticlesCategoriesSuccessResultContract>(result);
                    break;
                case IGetAllArticlesCategoriesErrorResultContract:
                    await context.RespondAsync<IGetAllArticlesCategoriesErrorResultContract>(result);
                    break;
            }
        }
    }
}
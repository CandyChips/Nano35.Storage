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
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetAllArticlesRequest>) _services
                .GetService(typeof(ILogger<LoggedGetAllArticlesRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedGetAllArticlesRequest(logger,
                    new ValidatedGetAllArticlesRequest(
                        new GetAllArticlesRequest(dbContext))).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IGetAllArticlesSuccessResultContract:
                    await context.RespondAsync<IGetAllArticlesSuccessResultContract>(result);
                    break;
                case IGetAllArticlesErrorResultContract:
                    await context.RespondAsync<IGetAllArticlesErrorResultContract>(result);
                    break;
            }
        }
    }
}
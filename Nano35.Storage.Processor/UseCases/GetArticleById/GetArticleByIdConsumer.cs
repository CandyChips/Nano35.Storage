using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetArticleById
{
    public class GetArticleByIdConsumer : 
        IConsumer<IGetArticleByIdRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetArticleByIdConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<IGetArticleByIdRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetArticleByIdRequest>) _services
                .GetService(typeof(ILogger<LoggedGetArticleByIdRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedGetArticleByIdRequest(logger,
                    new ValidatedGetArticleByIdRequest(
                        new GetArticleByIdRequest(dbContext))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IGetArticleByIdSuccessResultContract:
                    await context.RespondAsync<IGetArticleByIdSuccessResultContract>(result);
                    break;
                case IGetArticleByIdErrorResultContract:
                    await context.RespondAsync<IGetArticleByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllArticleBrands
{
    public class GetAllArticlesBrandsConsumer : 
        IConsumer<IGetAllArticlesBrandsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllArticlesBrandsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllArticlesBrandsRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetAllArticlesBrandsRequest>) _services
                .GetService(typeof(ILogger<LoggedGetAllArticlesBrandsRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedGetAllArticlesBrandsRequest(logger,
                    new ValidatedGetAllArticlesBrandsRequest(
                        new GetAllArticlesBrandsRequest(dbContext))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IGetAllArticlesBrandsSuccessResultContract:
                    await context.RespondAsync<IGetAllArticlesBrandsSuccessResultContract>(result);
                    break;
                case IGetAllArticlesBrandsErrorResultContract:
                    await context.RespondAsync<IGetAllArticlesBrandsErrorResultContract>(result);
                    break;
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleInfo
{
    public class UpdateArticleInfoConsumer : 
        IConsumer<IUpdateArticleInfoRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public UpdateArticleInfoConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateArticleInfoRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedUpdateArticleInfoRequest>) _services
                .GetService(typeof(ILogger<LoggedUpdateArticleInfoRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedUpdateArticleInfoRequest(logger,
                    new ValidatedUpdateArticleInfoRequest(
                        new UpdateArticleInfoRequest(dbContext))).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IUpdateArticleInfoSuccessResultContract:
                    await context.RespondAsync<IUpdateArticleInfoSuccessResultContract>(result);
                    break;
                case IUpdateArticleInfoErrorResultContract:
                    await context.RespondAsync<IUpdateArticleInfoErrorResultContract>(result);
                    break;
            }
        }
    }
}
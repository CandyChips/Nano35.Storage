using System.Threading.Tasks;
using MassTransit;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests;
using Nano35.Storage.Processor.Requests.CreateArticle;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Consumers
{
    public class CreateArticleConsumer : 
        IConsumer<ICreateArticleRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public CreateArticleConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        public async Task Consume(
            ConsumeContext<ICreateArticleRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<CreateArticleLogger>) _services.GetService(typeof(ILogger<CreateArticleLogger>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new CreateArticleLogger(logger,
                new CreateArticleValidator(
                    new CreateArticleRequest(dbContext))
                ).Ask(message);
            
            // Check response of create article request
            switch (result)
            {
                case ICreateArticleSuccessResultContract:
                    await context.RespondAsync<ICreateArticleSuccessResultContract>(result);
                    break;
                case ICreateArticleErrorResultContract:
                    await context.RespondAsync<ICreateArticleErrorResultContract>(result);
                    break;
            }
        }
    }
}
using System.Threading.Tasks;
using MassTransit;
using System;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests.CreateArticle;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Consumers
{
    /// <summary>
    /// Consumer accept a request contract type
    /// All consumers actions works by pipelines
    /// Implementation works with 3 steps
    /// 1. Setup DI services from IServiceProvider;
    /// 2. Explore message of request;
    /// 3. Building pipeline like a onion
    ///     '(PipeNode1(PipeNode2(PipeNode3(...).Ask()).Ask()).Ask()).Ask()';
    /// 4. Response pattern match of pipeline response;
    /// </summary>
    public class CreateArticleConsumer : 
        IConsumer<ICreateArticleRequestContract>
    {
        private readonly IServiceProvider _services;
        
        /// <summary>
        /// Consumer provide IServiceProvider from asp net core DI
        /// for registration services to pipe nodes
        /// </summary>
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
            var logger = (ILogger<LoggedCreateArticleRequest>) _services.GetService(typeof(ILogger<LoggedCreateArticleRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedCreateArticleRequest(logger,
                new ValidatedCreateArticleRequest(
                    new TransactedCreateArticleRequest(dbContext,
                        new CreateArticleRequest(dbContext)
                        ))).Ask(message, context.CancellationToken);
            
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
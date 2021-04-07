using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleCategory
{
    public class UpdateArticleCategoryConsumer : 
        IConsumer<IUpdateArticleCategoryRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public UpdateArticleCategoryConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateArticleCategoryRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IUpdateArticleCategoryRequestContract>) _services
                .GetService(typeof(ILogger<IUpdateArticleCategoryRequestContract>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedPipeNode<IUpdateArticleCategoryRequestContract, IUpdateArticleCategoryResultContract>(logger,
                    new ValidatedUpdateArticleCategoryRequest(
                        new UpdateArticleCategoryRequest(dbContext))).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IUpdateArticleCategorySuccessResultContract:
                    await context.RespondAsync<IUpdateArticleCategorySuccessResultContract>(result);
                    break;
                case IUpdateArticleCategoryErrorResultContract:
                    await context.RespondAsync<IUpdateArticleCategoryErrorResultContract>(result);
                    break;
            }
        }
    }
}
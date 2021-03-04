using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateCategoryName
{
    public class UpdateCategoryNameConsumer : 
        IConsumer<IUpdateCategoryNameRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public UpdateCategoryNameConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateCategoryNameRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedUpdateCategoryNameRequest>) _services.GetService(typeof(ILogger<LoggedUpdateCategoryNameRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedUpdateCategoryNameRequest(logger,
                    new ValidatedUpdateCategoryNameRequest(
                        new UpdateCategoryNameRequest(dbContext))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IUpdateCategoryNameSuccessResultContract:
                    await context.RespondAsync<IUpdateCategoryNameSuccessResultContract>(result);
                    break;
                case IUpdateCategoryNameErrorResultContract:
                    await context.RespondAsync<IUpdateCategoryNameErrorResultContract>(result);
                    break;
            }
        }
    }
}
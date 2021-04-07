using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleBrand
{
    public class UpdateArticleBrandConsumer : 
        IConsumer<IUpdateArticleBrandRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public UpdateArticleBrandConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IUpdateArticleBrandRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IUpdateArticleBrandRequestContract>) _services
                .GetService(typeof(ILogger<IUpdateArticleBrandRequestContract>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedPipeNode<IUpdateArticleBrandRequestContract, IUpdateArticleBrandResultContract>(logger,
                    new ValidatedUpdateArticleBrandRequest(
                        new UpdateArticleBrandRequest(dbContext))).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IUpdateArticleBrandSuccessResultContract:
                    await context.RespondAsync<IUpdateArticleBrandSuccessResultContract>(result);
                    break;
                case IUpdateArticleBrandErrorResultContract:
                    await context.RespondAsync<IUpdateArticleBrandErrorResultContract>(result);
                    break;
            }
        }
    }
}
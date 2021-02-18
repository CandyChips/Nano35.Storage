using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests.CreateCancellation;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Consumers
{
    public class CreateCancellationConsumer : 
        IConsumer<ICreateCancellationRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public CreateCancellationConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<ICreateCancellationRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedCreateCancellatioRequest>) _services.GetService(typeof(ILogger<LoggedCreateCancellatioRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedCreateCancellatioRequest(logger,
                    new ValidatedCreateCancellationRequest(
                        new TransactedCreateCancellationRequest(dbContext,
                            new CreateCancellationRequest(dbContext)))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case ICreateCancellationSuccessResultContract:
                    await context.RespondAsync<ICreateCancellationSuccessResultContract>(result);
                    break;
                case ICreateCancellationErrorResultContract:
                    await context.RespondAsync<ICreateCancellationErrorResultContract>(result);
                    break;
            }
        }
    }
}
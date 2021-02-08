using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests.CreateCancelation;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Consumers
{
    public class CreateCancelationConsumer : 
        IConsumer<ICreateCancelationRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public CreateCancelationConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<ICreateCancelationRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<CreateCancelationLogger>) _services.GetService(typeof(ILogger<CreateCancelationLogger>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new CreateCancelationLogger(logger,
                    new CreateCancelationValidator(
                        new CreateCancelationTransaction(dbContext,
                            new CreateCancelationRequest(dbContext)))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case ICreateCancelationSuccessResultContract:
                    await context.RespondAsync<ICreateCancelationSuccessResultContract>(result);
                    break;
                case ICreateCancelationErrorResultContract:
                    await context.RespondAsync<ICreateCancelationErrorResultContract>(result);
                    break;
            }
        }
    }
}
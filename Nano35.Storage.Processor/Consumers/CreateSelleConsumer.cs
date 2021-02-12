using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests.CreateSalle;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Consumers
{
    public class CreateSelleConsumer : 
        IConsumer<ICreateSelleRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public CreateSelleConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<ICreateSelleRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<CreateSelleLogger>) _services.GetService(typeof(ILogger<CreateSelleLogger>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new CreateSelleLogger(logger,
                    new CreateSelleValidator(
                        new CreateSelleTransaction(dbContext,
                            new CreateSelleRequest(dbContext)))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case ICreateSelleSuccessResultContract:
                    await context.RespondAsync<ICreateSelleSuccessResultContract>(result);
                    break;
                case ICreateSelleErrorResultContract:
                    await context.RespondAsync<ICreateSelleErrorResultContract>(result);
                    break;
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests.CreateSalle;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Consumers
{
    public class CreateSalleConsumer : 
        IConsumer<ICreateSalleRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public CreateSalleConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<ICreateSalleRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<CreateSalleLogger>) _services.GetService(typeof(ILogger<CreateSalleLogger>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new CreateSalleLogger(logger,
                    new CreateSalleValidator(
                        new CreateSalleTransaction(dbContext,
                            new CreateSalleRequest(dbContext)))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case ICreateSalleSuccessResultContract:
                    await context.RespondAsync<ICreateSalleSuccessResultContract>(result);
                    break;
                case ICreateSalleErrorResultContract:
                    await context.RespondAsync<ICreateSalleErrorResultContract>(result);
                    break;
            }
        }
    }
}
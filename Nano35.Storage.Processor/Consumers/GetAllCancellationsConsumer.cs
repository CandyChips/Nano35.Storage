using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests.GetAllCancellations;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Consumers
{
    public class GetAllCancellationsConsumer : 
        IConsumer<IGetAllCancellationsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllCancellationsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllCancellationsRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetAllCancellationsRequest>) _services
                .GetService(typeof(ILogger<LoggedGetAllCancellationsRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedGetAllCancellationsRequest(logger,
                    new ValidatedGetAllCancellationsRequest(
                        new GetAllCancellationsRequest(dbContext))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IGetAllCancellationsSuccessResultContract:
                    await context.RespondAsync<IGetAllCancellationsSuccessResultContract>(result);
                    break;
                case IGetAllCancellationsErrorResultContract:
                    await context.RespondAsync<IGetAllCancellationsErrorResultContract>(result);
                    break;
            }
        }
    }
}
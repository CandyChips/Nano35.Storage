using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests.GetMoveDetailsById;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Consumers
{
    public class GetMoveDetailsByIdConsumer : 
        IConsumer<IGetMoveDetailsByIdRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetMoveDetailsByIdConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<IGetMoveDetailsByIdRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetMoveDetailsByIdRequest>) _services
                .GetService(typeof(ILogger<LoggedGetMoveDetailsByIdRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedGetMoveDetailsByIdRequest(logger,
                    new ValidatedGetMoveDetailsByIdRequest(
                        new GetMoveDetailsByIdRequest(dbContext,bus))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IGetMoveDetailsByIdSuccessResultContract:
                    await context.RespondAsync<IGetMoveDetailsByIdSuccessResultContract>(result);
                    break;
                case IGetMoveDetailsByIdErrorResultContract:
                    await context.RespondAsync<IGetMoveDetailsByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}
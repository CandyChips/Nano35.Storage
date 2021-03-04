using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetSelleDetailsById
{
    public class GetSelleDetailsByIdConsumer : 
        IConsumer<IGetSelleDetailsByIdRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetSelleDetailsByIdConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<IGetSelleDetailsByIdRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetSelleDetailsByIdRequest>) _services
                .GetService(typeof(ILogger<LoggedGetSelleDetailsByIdRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedGetSelleDetailsByIdRequest(logger,
                    new ValidatedGetSelleDetailsByIdRequest(
                        new GetSelleDetailsByIdRequest(dbContext,bus))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IGetSelleDetailsByIdSuccessResultContract:
                    await context.RespondAsync<IGetSelleDetailsByIdSuccessResultContract>(result);
                    break;
                case IGetSelleDetailsByIdErrorResultContract:
                    await context.RespondAsync<IGetSelleDetailsByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}
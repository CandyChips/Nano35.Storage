using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetCancellationDetailsById
{
    public class GetCancellationDetailsByIdConsumer : 
        IConsumer<IGetCancellationDetailsByIdRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetCancellationDetailsByIdConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetCancellationDetailsByIdRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetCancellationDetailsByIdRequest>) _services
                .GetService(typeof(ILogger<LoggedGetCancellationDetailsByIdRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedGetCancellationDetailsByIdRequest(logger,
                    new ValidatedGetCancellationDetailsByIdRequest(
                        new GetCancellationDetailsByIdRequest(dbContext, bus))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IGetCancellationDetailsByIdSuccessResultContract:
                    await context.RespondAsync<IGetCancellationDetailsByIdSuccessResultContract>(result);
                    break;
                case IGetCancellationDetailsByIdErrorResultContract:
                    await context.RespondAsync<IGetCancellationDetailsByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllCancellationDetails
{
    public class GetAllCancellationDetailsConsumer : 
        IConsumer<IGetAllCancellationDetailsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllCancellationDetailsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllCancellationDetailsRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetAllCancellationDetailsRequest>) _services
                .GetService(typeof(ILogger<LoggedGetAllCancellationDetailsRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedGetAllCancellationDetailsRequest(logger,
                    new ValidatedGetAllCancellationDetailsRequest(
                        new GetAllCancellationDetailsRequest(dbContext))).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IGetAllCancellationDetailsSuccessResultContract:
                    await context.RespondAsync<IGetAllCancellationDetailsSuccessResultContract>(result);
                    break;
                case IGetAllCancellationDetailsErrorResultContract:
                    await context.RespondAsync<IGetAllCancellationDetailsErrorResultContract>(result);
                    break;
            }
        }
    }
}
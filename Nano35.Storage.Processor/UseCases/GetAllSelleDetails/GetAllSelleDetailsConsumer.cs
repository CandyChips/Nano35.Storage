using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllSelleDetails
{
    public class GetAllSelleDetailsConsumer : 
        IConsumer<IGetAllSelleDetailsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllSelleDetailsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllSelleDetailsRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllSelleDetailsRequest>) _services
                .GetService(typeof(ILogger<LoggedGetAllSelleDetailsRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedGetAllSelleDetailsRequest(logger,
                    new ValidatedGetAllSelleDetailsRequest(
                        new GetAllSelleDetailsRequest(dbContext, bus))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IGetAllSelleDetailsSuccessResultContract:
                    await context.RespondAsync<IGetAllSelleDetailsSuccessResultContract>(result);
                    break;
                case IGetAllSelleDetailsErrorResultContract:
                    await context.RespondAsync<IGetAllSelleDetailsErrorResultContract>(result);
                    break;
            }
        }
    }
}
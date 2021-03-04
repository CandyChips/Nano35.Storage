using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllMoves
{
    public class GetAllMovesConsumer : 
        IConsumer<IGetAllMovesRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllMovesConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllMovesRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllMovesRequest>) _services
                .GetService(typeof(ILogger<LoggedGetAllMovesRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedGetAllMovesRequest(logger,
                    new ValidatedGetAllMovesRequest(
                        new GetAllMovesRequest(dbContext, bus))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IGetAllMovesSuccessResultContract:
                    await context.RespondAsync<IGetAllMovesSuccessResultContract>(result);
                    break;
                case IGetAllMovesErrorResultContract:
                    await context.RespondAsync<IGetAllMovesErrorResultContract>(result);
                    break;
            }
        }
    }
}
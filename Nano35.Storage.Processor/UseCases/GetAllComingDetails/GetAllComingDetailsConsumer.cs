using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllComingDetails
{
    public class GetAllComingDetailsConsumer : 
        IConsumer<IGetAllComingDetailsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllComingDetailsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllComingDetailsRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllComingDetailsRequest>) _services.GetService(typeof(ILogger<LoggedGetAllComingDetailsRequest>));

            var message = context.Message;

            var result = await new LoggedGetAllComingDetailsRequest(logger,
                        new ValidatedGetAllComingDetailsRequest(
                            new GetAllComingDetailsRequest(dbContext, bus))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllComingDetailsSuccessResultContract:
                    await context.RespondAsync<IGetAllComingDetailsSuccessResultContract>(result);
                    break;
                case IGetAllComingDetailsErrorResultContract:
                    await context.RespondAsync<IGetAllComingDetailsErrorResultContract>(result);
                    break;
            }
        }
    }
}
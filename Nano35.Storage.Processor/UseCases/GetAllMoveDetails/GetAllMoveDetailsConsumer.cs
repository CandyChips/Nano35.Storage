using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllMoveDetails
{
    public class GetAllMoveDetailsConsumer : 
        IConsumer<IGetAllMoveDetailsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllMoveDetailsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllMoveDetailsRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<IGetAllMoveDetailsRequestContract>) _services
                .GetService(typeof(ILogger<IGetAllMoveDetailsRequestContract>));
            var message = context.Message;
            var result =
                await new LoggedPipeNode<IGetAllMoveDetailsRequestContract, IGetAllMoveDetailsResultContract>(logger,
                    new GetAllMoveDetailsRequest(dbContext, bus)).Ask(message, context.CancellationToken);
            switch (result)
            {
                case IGetAllMoveDetailsSuccessResultContract:
                    await context.RespondAsync<IGetAllMoveDetailsSuccessResultContract>(result);
                    break;
                case IGetAllMoveDetailsErrorResultContract:
                    await context.RespondAsync<IGetAllMoveDetailsErrorResultContract>(result);
                    break;
            }
        }
    }
}
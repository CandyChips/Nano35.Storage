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
            var result =
                await new LoggedUseCasePipeNode<IGetAllMoveDetailsRequestContract, IGetAllMoveDetailsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllMoveDetailsRequestContract>)) as ILogger<IGetAllMoveDetailsRequestContract>,
                        new GetAllMoveDetailsRequest(                            
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}
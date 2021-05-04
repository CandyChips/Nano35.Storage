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
            var result =
                await new LoggedUseCasePipeNode<IGetAllMovesRequestContract, IGetAllMovesResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllMovesRequestContract>)) as ILogger<IGetAllMovesRequestContract>,
                        new GetAllMovesRequest(                            
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}